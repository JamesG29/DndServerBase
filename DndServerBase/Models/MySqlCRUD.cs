using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.IO;
using System.CodeDom;

namespace DndServerBase.Models
{
    public class MySqlCRUD
    {
        private static bool OpenDBConnection(MySqlConnection connection)
        {


            try
            {
                connection.Open();
                Console.WriteLine("Opened DataBase");
                return true;
            }
            //catch the error here as 'ex'
            catch (MySqlException ex)
            {
                //error 0: cant connect to server
                //error 1045: invalud username or password
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Can't connect to server. Contact Supprt");
                        break;
                    case 1045:
                        Console.WriteLine("Invalid user/password, try again.");
                        break;
                }

                return false;
            }
        }

        private static bool CloseDBConnection(MySqlConnection connection)
        {
            try
            {
                connection.Close();
                Console.WriteLine("Closed Database");
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public static void InsertRowDB(MySqlConnection connection, string table,
               List<string> tableArgTypes, List<string> tableArgVals)
        {
           
            //tablrArgumentsString starts Empty
            string tableArgumentsString = "";
            //add all arguments that will be in the inserted into the Table
            for (int i = 0; i < tableArgTypes.Count; i++)
            {
                if (i == tableArgTypes.Count - 1)
                {
                    tableArgumentsString += $"{tableArgTypes[i]}";
                }
                else
                {
                    tableArgumentsString += $"{tableArgTypes[i]}, ";
                }
            }


            //tableValuesString starts Empty
            string tableValuesString = "";
            //add all values that will be inserted into the table
            for (int i = 0; i < tableArgVals.Count; i++)
            {
                if (i == tableArgVals.Count - 1 && !(tableArgVals[i] == "Image" || tableArgVals[i] == "image"))
                {
                    //The worlds DEFAULT and NULL are not strings and there for must not contain quotation marks
                    if (tableArgVals[i] == "DEFAULT" || tableArgVals[i] == "NULL")
                    {
                        tableValuesString += $"{tableArgVals[i]}";
                    }
                    else
                    {
                        tableValuesString += $"'{tableArgVals[i]}'";
                    }
                }
                else if(tableArgVals[i] == "Image" || tableArgVals[i] == "image")
                {

                }
                else
                {
                    if (tableArgVals[i] == "DEFAULT" || tableArgVals[i] == "NULL")
                    {
                        tableValuesString += $"{tableArgVals[i]}, ";
                    }
                    else
                    {
                        tableValuesString += $"'{tableArgVals[i]}', ";
                    }
                }
            }



            string query = $"INSERT INTO {table} ({tableArgumentsString}) VALUES({tableValuesString})";
            Console.WriteLine($"Inputed Query: {query}");
            //open the connection to insert
                //create the commadn to input the queary intp MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //execute the command
                cmd.ExecuteNonQuery();
                Console.WriteLine("Executed DB Insert Command");

                //end connection(close)
                CloseDBConnection(connection);

        }

        public static void UpdateRowDB(MySqlConnection connection, string table, List<string> tableCollumns, List<string> tableVals,
        string locateCollumn, string locateValue)
        {
            //initially define set and where as empty strings
            string set = "";
            string where = "";

            //SET
            //create the set string base off of functions inputs
            for (int i = 0; i < tableCollumns.Count; i++)
            {
                //this would be the last one in the last, so dont add a comma or space at the end
                if (i == tableCollumns.Count - 1)
                {
                    if (tableVals[i] == "DEFAULT" || tableVals[i] == "NULL")
                    {
                        //remove quitation marks of DEFAULT or NULL
                        set += $"{tableCollumns[i]}={tableVals[i]}";
                    }
                    else
                    {
                        set += $"{tableCollumns[i]}='{tableVals[i]}'";
                    }
                }
                else
                {
                    if (tableVals[i] == "DEFAULT" || tableVals[i] == "NULL")
                    {
                        //remove quitation marks of DEFAULT or NULL
                        set += $"{tableCollumns[i]}={tableVals[i]}, ";
                    }
                    else
                    {
                        set += $"{tableCollumns[i]}='{tableVals[i]}', ";
                    }
                }

            }

            //WHERE
            if (locateValue == "DEFAULT" || locateValue == "NULL")
            {
                //remove quitation marks of DEFAULT or NULL
                where = $"{locateCollumn}={locateValue}";
            }
            else
            {
                where = $"{locateCollumn}='{locateValue}'";
            }


            //EXAMPLE query: "UPDATE user SET name='Jeffery', age='43' WHERE userid='12'"
            string query = $"UPDATE {table} SET {set} WHERE {where}";
            Console.WriteLine("UPDATE QUERY: " + query);

            if (OpenDBConnection(connection) == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                CloseDBConnection(connection);

            }
        }

        public static void DeleteRowDB(MySqlConnection connection, string table, string locateCollumn, string locateValue)
        {

            string where = "";
            //WHERE
            if (locateValue == "DEFAULT" || locateValue == "NULL")
            {
                //make sure that DEFAULT and NULL aren't enetered as mysql strings
                where = $"{locateCollumn}={locateValue}";
            }
            else
            {
                where = $"{locateCollumn}='{locateValue}'";
            }
            string query = $"DELETE FROM {table} WHERE {where}";
            Console.WriteLine("DELETE QUERY: " + query);

            if (OpenDBConnection(connection))
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                CloseDBConnection(connection);
            }
        }
    
        public static List<string>[] SelectAllRowsDB(MySqlConnection connection, string table, List<string> tableArgTypes)
        {
            //create the query
            string query = $"SELECT * FROM {table}";
            //create temp list to eventually send as our actual return list
            List<string>[] templist = new List<string>[tableArgTypes.Count];
            for(int i = 0; i < tableArgTypes.Count; i++)
            {
                templist[i] = new List<string>();
            }


            if (OpenDBConnection(connection))
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataRead = cmd.ExecuteReader();

                while (dataRead.Read())
                {
                    for(int i = 0; i < templist.Length; i++)
                    {
                        templist[i].Add(dataRead[tableArgTypes[i]] + "");
                    }
                }
                //end the data read once we have finished reading
                dataRead.Close();
                //close the connection
                CloseDBConnection(connection);

                //return our string array list
                return templist;
            }
            else
            {
                //return an empty array list
                return templist;
            }


        }
    
        public static bool IsFullNameTaken(MySqlConnection connection, string table, string fullname)
        {
            //create query
            string query = $"SELECT  FROM {table}";

            if (OpenDBConnection(connection))
            {
                Console.WriteLine("once");
                //create a mysqlcommand to send the query through and then read through the query data
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataRead = cmd.ExecuteReader();

                while (dataRead.Read())
                {
                    //look for an equivalance of our fullname to one already in the database table
                    if ((dataRead["npcfullname"] + "") == fullname)
                    {
                        //close up all connections
                        CloseDBConnection(connection);
                        dataRead.Close();
                        //the full name is already taken
                        return true;
                    }
                }
                //close up all connections
                CloseDBConnection(connection);
                dataRead.Close();
                //the fullname has yet to be found, it should be available
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}