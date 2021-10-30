using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Interfaces;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepsRepository : IRepository<Keep>
  {
      private readonly IDbConnection _db;

      public KeepsRepository(IDbConnection db)
      {
          _db = db;
      }
    public Keep Create(Keep data)
    {
      string sql = @"
      INSERT INTO keeps(name, description, img, views, shares, keeps, creatorId)
      VALUES (@Name, @Description, @Img, @Views, @Shares, @Keeps, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1";
      var rowsAffected = _db.Execute(sql, new { id });
      if(rowsAffected == 0) 
      { throw new Exception("No Keep found");
    }
    }

    public Keep Edit(Keep data)
    {
        string sql = @"
        UPDATE keeps SET
        name = @Name,
        description = @Description,
        img = @Img
        WHERE id = @Id LIMIT 1;
        ";
        var rowsAffected = _db.Execute(sql, data);
        if(rowsAffected == 0) 
        { throw new Exception("No Keep found");
        }
        return data;
    }

    public List<Keep> Get()
    {
      string sql = "SELECT * FROM keeps";
      return _db.Query<Keep>(sql).ToList();
    }

    public Keep Get(int id)
    {
     string sql = @"SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a on a.id = k.creatorId
      WHERE k.id = @id;";
      return _db.Query<Keep, Profile, Keep>(sql, (k, a) => {
        k.Creator = a;
        return k;
      }, new { id }).FirstOrDefault();
    }
  }
}