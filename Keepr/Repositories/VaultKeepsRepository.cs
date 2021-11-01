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
    public VaultKeep Create(VaultKeep newVaultKeep)
    {
      string sql = @"
      INSERT INTO vaultkeeps (keepId, vaultId, creatorId)
      VALUES (@KeepId, @VaultId, @CreatorId);
      SELECT LAST_INSERT_ID();";
      var id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      newVaultKeep.Id = id;
      return newVaultKeep;
    }

    public VaultKeep GetById(int vaultKeepId)
    {
      string sql = @"SELECT
      v.*,
      a.*
      FROM vaultkeeps v
      JOIN accounts a on a.id = v.creatorId
      WHERE id = @vaultKeepId;";
      return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (v, a) => { v.Creator = a; return v; }, new { vaultKeepId }).FirstOrDefault();
    }

    public void Delete(int id)
    {
      string sql = @"DELETE FROM vaultkeeps WHERE id = @id LIMIT 1";
      var rowsAffected = _db.Execute(sql, new { id});
      if(rowsAffected == 0)
      {
        throw new Exception("No vaultkeep found with that id");
      }
    }

     public List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
    {
      var sql = @"
      SELECT
      vk.id as vaultKeepId,
      vk.keepId as keepId,
      v.*,
      k.*
      FROM vaultkeeps vk
      JOIN accounts a on a.id = vk.creatorId
      JOIN vaults v on v.id = @vaultId
      JOIN keeps k ON k.id = vk.keepId
      WHERE vk.vaultId = @vaultId;
      ";
      return _db.Query<VaultKeepViewModel>(sql, new { vaultId }).ToList();
    }
  }
}

 
  //     vk.vaultId as vaultId,
  //     k.creatorId as creatorId,
  //     k.name as name,
  //     k.description as description,
  //     k.views as views,
  //     k.shares as shares,
  //     k.keeps as keeps,
  //     k.img as img,
  //     v.isPrivate as isPrivate