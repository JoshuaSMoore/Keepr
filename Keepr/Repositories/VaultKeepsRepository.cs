using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    public VaultKeep Create(VaultKeep vaultKeepData)
    {
      string sql = @"
      INSERT INTO vaultkeeps (keepId, vaultId)
      VALUES (@KeepId, @VaultId);
      SELECT LAST_INSERT_ID();";
      var id = _db.ExecuteScalar<int>(sql, vaultKeepData);
      vaultKeepData.Id = id;
      return vaultKeepData;
    }

    public VaultKeep GetById(int vaultKeepId)
    {
      string sql = @"SELECT
      v.*,
      a.*
      FROM vaultkeeps v
      JOIN accounts a on a.id = v.CreatorId
      WHERE v.id = @id;";
      return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { vaultKeepId }).FirstOrDefault();
    }

    public void Delete(int vaultKeepId)
    {
      string sql = @"DELETE FROM vaultkeeps WHERE id = @id LIMIT 1";
      var rowsAffected = _db.Execute(sql, new { id = vaultKeepId });
      if(rowsAffected == 0)
      {
        throw new Exception("No vaultkeep found with that id");
      }
    }

    public List<VaultKeep> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
      SELECT
      vk.*,
      k.*
      FROM vaultkeeps vk
      JOIN keeps k ON k.id = vk.keepId
      WHERE vk.vaultId = @vaultId";

      return _db.Query<VaultKeep, Keep, VaultKeep>(sql, (vk, k) =>
      {
        vk.Keep = k;
        return vk;
      }, new { vaultId }).ToList();
    }
  }
}