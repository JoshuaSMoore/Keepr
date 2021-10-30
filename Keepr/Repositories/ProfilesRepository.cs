using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class ProfilesRepository
  {
       private readonly IDbConnection _db;

      public ProfilesRepository(IDbConnection db)
      {

          _db = db;
      }
      public List<Keep> GetKeepByProfile(string profileId)
    {
      string sql = @"
      SELECT
      *
      FROM keeps k
      WHERE k.creatorId = @profileId
      ";
      return _db.Query<Keep>(sql, new { profileId }).ToList();
    }

      public Profile Get(string profileId)
    {
      var sql = @"
      SELECT 
      *
      FROM
      accounts
      ";
      return _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
    }
        public List<Vault> GetVaultsByProfile(string profileId)
    {
      string sql = @"
      SELECT
      *
      FROM vaults v
      WHERE k.creatorId = @profileId
      ";
      return _db.Query<Vault>(sql, new { profileId }).ToList();
    }
  }
}