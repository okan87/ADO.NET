# ADO.NET
c#MVC with Ado.Net

Connection to the database was made with AdoNet.

SqlCommand cmd = new SqlCommand("Select * From Kitap", con);

SqlDataReader sqlDataReader = cmd.ExecuteReader();

*-*-*-SqlDataAdapter uses for convert

SqlDataAdapter adapter = new SqlDataAdapter();
                
