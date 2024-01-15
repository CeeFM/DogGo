using DogGo.Models;
using Microsoft.Data.SqlClient;

namespace DogGo.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly IConfiguration _config;
        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public WalksRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walks> GetAllWalks()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id, w.Date, w.Duration, w.WalkerId, w.DogId, wa.Id as WalkerRealId, wa.Name as WalkerName, wa.ImageUrl, wa.NeighborhoodId, d.Id as DogId, d.Name as DogName, d.OwnerId, d.Breed, d.Notes, d.ImageUrl as DogImage
                        FROM Walks w
                        LEFT JOIN Walker wa ON w.WalkerId = wa.Id
                        LEFT JOIN Dog d ON w.DogId = d.Id
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walks> walks = new List<Walks>();
                    while (reader.Read())
                    {
                        Walks walk = new Walks
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            Walker = new Walker
                            {
                                Name = reader.GetString(reader.GetOrdinal("WalkerName")),
                                Id = reader.GetInt32(reader.GetOrdinal("WalkerRealId")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                            },
                            Dog = new Dog
                            {
                                Name = reader.GetString(reader.GetOrdinal("DogName")),
                                Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Notes = "",
                                ImageUrl = "",
                            }
                        };

                        walks.Add(walk);
                    }

                    reader.Close();

                    return walks;
                }
            }
        }

        public Walks GetWalksById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id, w.Date, w.Duration, w.WalkerId, w.DogId, wa.Id as WalkerRealId, wa.Name as WalkerName, wa.ImageUrl, wa.NeighborhoodId, d.Id as DogId, d.Name as DogName, d.OwnerId, d.Breed, d.Notes, d.ImageUrl as DogImage
                        FROM Walks w
                        LEFT JOIN Walker wa ON w.WalkerId = wa.Id
                        LEFT JOIN Dog d ON w.DogId = d.Id
                        WHERE w.Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Walks walk = new Walks
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            Walker = new Walker
                            {
                                Name = reader.GetString(reader.GetOrdinal("WalkerName")),
                                Id = reader.GetInt32(reader.GetOrdinal("WalkerRealId")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                            },
                            Dog = new Dog
                            {
                                Name = reader.GetString(reader.GetOrdinal("DogName")),
                                Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Notes = "",
                                ImageUrl = "",
                            }
                        };

                        reader.Close();
                        return walk;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }

        public List<Walks> GetWalksByWalker(int walkerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id, w.Date, w.Duration, w.WalkerId, w.DogId, wa.Id as WalkerRealId, wa.Name as WalkerName, wa.ImageUrl, wa.NeighborhoodId, d.Id as DogId, d.Name as DogName, d.OwnerId, d.Breed, d.Notes, d.ImageUrl as DogImage, o.Id as OwnerRealId, o.Email, o.Name as OwnerName, o.Address, o.NeighborhoodId as OwnerNeighborhood, o.Phone
                        FROM Walks w
                        LEFT JOIN Walker wa ON w.WalkerId = wa.Id
                        LEFT JOIN Dog d ON w.DogId = d.Id
                        LEFT JOIN Owner o ON d.OwnerId = o.Id
                        WHERE wa.Id = @id
                        ORDER BY OwnerName
            ";

                    cmd.Parameters.AddWithValue("@id", walkerId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walks> walks = new List<Walks>();
                    while (reader.Read())
                    {
                        Walks walk = new Walks
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            Walker = new Walker
                            {
                                Name = reader.GetString(reader.GetOrdinal("WalkerName")),
                                Id = reader.GetInt32(reader.GetOrdinal("WalkerRealId")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                            },
                            Dog = new Dog
                            {
                                Name = reader.GetString(reader.GetOrdinal("DogName")),
                                Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Owner = new Owner
                                {
                                    Name = reader.GetString(reader.GetOrdinal("OwnerName")),
                                    Id = reader.GetInt32(reader.GetOrdinal("OwnerRealId")),
                                    Address = reader.GetString(reader.GetOrdinal("Address")),
                                    NeighborhoodId = reader.GetInt32(reader.GetOrdinal("OwnerNeighborhood")),
                                    Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                }
                            }
                        };

                        walks.Add(walk);
                    }

                    reader.Close();

                    return walks;
                }
            }
        }

        public void AddWalk(Walks walk)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Walks (Date, WalkerId, DogId, Duration)
                    OUTPUT INSERTED.ID
                    VALUES (@date, @walkerid, @dogid, @duration);
                ";

                    cmd.Parameters.AddWithValue("@date", walk.Date);
                    cmd.Parameters.AddWithValue("@walkerid", walk.WalkerId);
                    cmd.Parameters.AddWithValue("@dogid", walk.DogId);
                    cmd.Parameters.AddWithValue("@duration", walk.Duration);

                    int id = (int)cmd.ExecuteScalar();

                    walk.Id = id;
                }
            }
        }

        public void DeleteWalk(int walkId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Walks
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", walkId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
