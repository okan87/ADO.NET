# ADO.NET
c#MVC with Ado.Net

Connection to the database was made with AdoNet.

SqlCommand cmd = new SqlCommand("Select * From Kitap", con);

SqlDataReader sqlDataReader = cmd.ExecuteReader();

SqlDataAdapter adapter = new SqlDataAdapter(); ==> You can act on the DataTable.
                
