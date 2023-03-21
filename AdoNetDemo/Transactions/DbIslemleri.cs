using AdoNetDemo.Models;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetDemo.Transactions
{
    public class DbIslemleri
    {
        private SqlConnectionStringBuilder sqlConnectionStringBuilder;
        private SqlConnection con;
        public DbIslemleri()
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.ConnectionString = 
                "Server=DESKTOP-312A3RM\\SQLEXPRESS;Database=EFCoreDemoDB;Trusted_Connection=True;Encrypt=False";
            con = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        }

        public int ExecuteCommand(SqlCommand cmd)
        {
            int result = 0;
            try
            {
                cmd.Connection.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally 
            {
                cmd.Connection.Close(); 
            }
            return result;
        }

        public List<Kitap> VeriGetirDR()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * From Kitap", con);
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                List<Kitap> kitaps = new List<Kitap>();
                while (sqlDataReader.Read())
                {
                    Kitap kitap = new Kitap
                    {
                        Name = sqlDataReader["Name"].ToString(),
                        Type = sqlDataReader["Type"].ToString(),
                        Price = Convert.ToDecimal(sqlDataReader["Price"].ToString()),
                        Yazar = Convert.ToInt32(sqlDataReader["Yazar"].ToString())
                    };
                    kitaps.Add(kitap);
                }
                con.Close();
                return kitaps;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
        public DataTable VeriGetirDA()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * From Kitap", con);
            DataTable dataTable = new DataTable();
            int result = sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public int VeriEkle(Kitap kitap)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            //cmd.CommandText = "Insert Into Kitap Values(" + kitap.Name + "," + kitap.Type + "," + kitap.Price + "," + kitap.Yazar +")";
            cmd.CommandText = "Insert Into Kitap Values(@Name, @Type, @Price, @Yazar )";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Name", kitap.Name);
            param[1] = new SqlParameter("@Type", kitap.Type);
            param[2] = new SqlParameter("@Price", kitap.Price);
            param[3] = new SqlParameter("@Yazar", kitap.Yazar);

            cmd.Parameters.AddRange(param);
            int result = ExecuteCommand(cmd);
            return result;

        }
        public int VeriGuncelle(string column, string value, int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            //cmd.CommandText = "Insert Into Kitap Values(" + kitap.Name + "," + kitap.Type + "," + kitap.Price + "," + kitap.Yazar +")";
            cmd.CommandText = "Update Kitap Set @Collumn = @Value Where ID = @ID";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Column", column);
            param[1] = new SqlParameter("@Value", value);
            param[2] = new SqlParameter("@id",id);

            cmd.Parameters.AddRange(param);
            int result = ExecuteCommand(cmd);
            return result;

        }
        public int VeriSil(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            //cmd.CommandText = "Insert Into Kitap Values(" + kitap.Name + "," + kitap.Type + "," + kitap.Price + "," + kitap.Yazar +")";
            cmd.CommandText = "Delete From Kitap Where ID=@ID";
            SqlParameter param = new SqlParameter("@ID", id);
            cmd.Parameters.Add(param);
            int result = ExecuteCommand(cmd);
            return result;

        }

    }
}
