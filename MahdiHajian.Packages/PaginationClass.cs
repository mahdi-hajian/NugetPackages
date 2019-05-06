using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MahdiHajian.Packages
{
    public class PaginationClass<TObject>
    {
        public static DTResult<TObject> Pagination(DTParameters parameters, string searchString, string connectionString, string tableName)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                var search = "";
                for (int i = 1; i < parameters.Columns.Count() - 1; i++)
                {
                    if (!string.IsNullOrEmpty(parameters.Columns[i].Search.Value))
                    {
                        search += parameters.Columns[i].Data + @" like CONCAT('%',N'" + parameters.Columns[i].Search.Value + "','%')|";
                    }
                }

                if (!string.IsNullOrEmpty(search))
                {
                    var searchItems = search.Trim().Split('|').ToList();
                    searchItems.RemoveAt(searchItems.Count - 1);
                    searchItems.ToArray();
                    search = "";
                    if (searchItems.Count > 1)
                    {
                        foreach (var item in searchItems)
                        {
                            if (searchItems.Last() != item)
                            {
                                search += item + " AND ";
                            }
                            else
                            {
                                search += item;
                            }
                        }
                    }
                    else
                    {
                        search += searchItems[0].ToString();
                    }
                    if (searchString != "")
                        search = @" where " + searchString + " AND " + search;
                    else
                        search = @" where " + search;

                }
                else
                {
                    if (searchString != "")
                        search = @" where " + searchString;
                }

                var paginationString = @" OFFSET @start ROWS FETCH NEXT @length ROWS ONLY; ";
                var sql = "";
                if (searchString != "")
                {
                    sql = @"select count(id) from " + tableName + " where " + searchString +
                          " select count(*) from " + tableName + search +
                          @" select * from " + tableName
                          + search + @" order by " + parameters.SortOrder + paginationString;
                }
                else
                {
                    sql = @"select count(id) from " + tableName +
                          " select count(*) from " + tableName + search +
                          @" select * from " + tableName
                          + search + @" order by " + parameters.SortOrder + paginationString;
                }
                var query = connection.QueryMultiple(sql, new { searchString = parameters.Search.Value, start = parameters.Start, length = parameters.Length });
                var countAll = query.Read<int>();
                var countFilter = query.Read<int>();
                var models = query.Read<TObject>();

                return new DTResult<TObject>
                {
                    data = models.ToList(),
                    draw = parameters.Draw,
                    recordsTotal = countAll.First(),
                    recordsFiltered = countFilter.First()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
