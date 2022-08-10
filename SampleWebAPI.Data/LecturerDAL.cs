using Dapper;
using SampleWebAPI.Domain;
using System.Data.SqlClient;

namespace SampleWebAPI.Data
{
    public class LecturerDAL
    {
        private string GetConnString()
        {
            return @"Data Source=.\SQLEXPRESS;
                     Initial Catalog=SampleAdoDb;
                     Integrated Security=SSPI";
        }

        /*public IEnumerable<Lecturer> GetAll()
        {
            List<Lecturer> lstLecturers = new List<Lecturer>();
            using(SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"select * from Lecturers order by Nama asc";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                
                if (dr.HasRows)
                {
                    while(dr.Read())
                    {
                        lstLecturers.Add(new Lecturer()
                        {
                            Nik = dr["Nik"].ToString(),
                            Nama = dr["Nama"].ToString(),
                            Alamat = dr["Alamat"].ToString(),
                            Telp = dr["Telp"].ToString()
                        });
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return lstLecturers;
        }*/

        public IEnumerable<Lecturer> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"select * from Lecturers order by Nama asc";
                var results = conn.Query<Lecturer>(strSql);
                return results;
            }
        }

        /*public Lecturer GetById(string nik)
        {
            Lecturer lecturer = new Lecturer();
            using(SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"select * from Lecturers where Nik=@Nik";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Nik", nik);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    dr.Read();
                    lecturer.Nik = dr["Nik"].ToString();
                    lecturer.Nama = dr["Nama"].ToString();
                    lecturer.Alamat = dr["Alamat"].ToString();
                    lecturer.Telp = dr["Telp"].ToString();
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return lecturer;
        }*/

        public Lecturer GetById(string nik)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"select * from Lecturers where Nik=@Nik";
                var param = new { Nik = nik };
                var result = conn.QueryFirst<Lecturer>(strSql, param);
                return result;
            }
        }

        /*public void Insert(Lecturer lecturer)
        {
            using(SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"insert into Lecturers(Nik,Nama,Alamat,Telp) 
                                  values(@Nik,@Nama,@Alamat,@Telp)";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Nik", lecturer.Nik);
                cmd.Parameters.AddWithValue("@Nama", lecturer.Nama);
                cmd.Parameters.AddWithValue("@Alamat", lecturer.Alamat);
                cmd.Parameters.AddWithValue("@Telp",lecturer.Telp);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result != 1)
                        throw new Exception("Gagal menambahkan data");
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627)
                        throw new Exception($"Data {lecturer.Nik} sudah ada, inputkan Nik yang lain..");
                    else
                        throw new Exception($"{sqlEx.Message} - {sqlEx.Number}");
                }
                catch(Exception ex)
                {
                    throw new Exception($"{ex.Message}");
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }*/

        public void Insert(Lecturer lecturer)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"insert into Lecturers(Nik,Nama,Alamat,Telp) 
                                  values(@Nik,@Nama,@Alamat,@Telp)";
                var param = new { Nik = lecturer.Nik, Nama = lecturer.Nama, 
                    Alamat = lecturer.Alamat, Telp = lecturer.Telp };
                try
                {
                    int result = conn.Execute(strSql, param);
                    if (result != 1)
                        throw new Exception($"Gagal untuk menambah data lecturer {lecturer.Nama}");
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"{sqlEx.Number} - {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message}");
                }
            }
        }


        /*public void Update(Lecturer lecturer)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"update Lecturers set Nama=@Nama,Alamat=@Alamat,Telp=@Telp 
                                  where Nik=@Nik";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Nama", lecturer.Nama);
                cmd.Parameters.AddWithValue("@Alamat", lecturer.Alamat);
                cmd.Parameters.AddWithValue("@Telp", lecturer.Telp);
                cmd.Parameters.AddWithValue("@Nik", lecturer.Nik);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result != 1)
                        throw new Exception($"Update data {lecturer.Nama} gagal");

                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"{sqlEx.Message} - {sqlEx.Number}");
                }
                catch(Exception ex)
                {
                    throw new Exception($"{ex.Message}");
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }*/

        public void Update(Lecturer lecturer)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"update Lecturers set Nama=@Nama,Alamat=@Alamat,Telp=@Telp 
                                  where Nik=@Nik";
                var param = new
                {
                    Nama = lecturer.Nama,
                    Alamat = lecturer.Alamat,
                    Telp = lecturer.Telp,
                    Nik = lecturer.Nik
                };

                try
                {
                    int result = conn.Execute(strSql, param);
                    if (result != 1)
                        throw new Exception($"Gagal untuk mengupdate data lecturer {lecturer.Nama}");
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"{sqlEx.Number} - {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message}");
                }
            }
        }


        /*public void Delete(string Nik)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"delete from Lecturers where Nik=@Nik";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Nik", Nik);
                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if(result!=1)
                    {
                        throw new Exception($"Gagal delete data dengan nik {Nik}");
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"{sqlEx.Message} - {sqlEx.Number}");
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }*/

        public void Delete(string Nik)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"delete from Lecturers where Nik=@Nik";
                var param = new {Nik= Nik };
                try
                {
                    var result = conn.Execute(strSql, param);
                    if (result != 1)
                        throw new Exception($"Gagal untuk mendelete data nik {Nik}");
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"{sqlEx.Number} - {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message}");
                }
            }
        }

        /*public IEnumerable<Lecturer> GetByNama(string nama)
        {
            List<Lecturer> listLecturers = new List<Lecturer>();
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"select * from Lecturers where Nama like @Nama";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Nama", $"%{nama}%");
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        listLecturers.Add(new Lecturer
                        {
                            Nik = dr["Nik"].ToString(),
                            Nama = dr["Nama"].ToString(),
                            Alamat = dr["Alamat"].ToString(),
                            Telp = dr["Telp"].ToString()
                        });
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return listLecturers;
        }*/

        public IEnumerable<Lecturer> GetByNama(string nama)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"select * from Lecturers where Nama like @Nama";
                var param = new { Nama = $"%{nama}%" };
                var results = conn.Query<Lecturer>(strSql, param);
                return results;
            }
        }

    }
}
