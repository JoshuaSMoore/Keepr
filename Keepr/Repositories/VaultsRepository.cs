using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Interfaces;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository : IRepository<Vault>
  {
      private readonly IDbConnection _db;

      public VaultsRepository(IDbConnection db)
      {
          _db = db;
      }
    public Vault Create(Vault data)
    {
      string sql = @"
      INSERT INTO vaults(name, description, isPrivate, creatorId)
      VALUES (@Name, @Description, @IsPrivate, @CreatorId);
      SELECT LAST_INSERT_ID();";
      var id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
      var rowsAffected = _db.Execute(sql, new { id });
      if(rowsAffected == 0) 
      { throw new Exception("No vault found");
      }
    }
     public Vault GetNonPrivate(int id)
    {
      string sql = @"SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a on a.id = v.creatorId
      WHERE v.id = @Id;";
      return _db.Query<Vault, Profile, Vault>(sql, (v, a) => { v.Creator = a; return v; }, new { id }).FirstOrDefault();
    }

    public Vault Edit(Vault data)
    {
      string sql = @"UPDATE vaults
      SET
      name = @Name,
      description = @Description,
        isPrivate = @IsPrivate
        WHERE id = @Id LIMIT 1;";
        var rowsAffected = _db.Execute(sql, data);
        if(rowsAffected == 0) 
        { throw new Exception("No vault found");
        } return data;
    }

    public List<Vault> Get()
    {
      string sql = "SELECT * FROM vaults";
      return _db.Query<Vault>(sql).ToList();
    }
    

    public Vault Get(int id)
    {
      string sql = @"SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a on a.id = v.creatorId
      WHERE v.id = @Id;";
      return _db.Query<Vault, Profile, Vault>(sql, (v, a) => { v.Creator = a; return v; }, new { id }).FirstOrDefault();
    }
  }
}