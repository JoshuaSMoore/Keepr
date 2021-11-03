using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
      private readonly KeepsRepository _keepsRepository;
      public KeepsService(KeepsRepository keepsRepository)
      {
          _keepsRepository = keepsRepository;
      }
    public List<Keep> GetAll()
    {
      return _keepsRepository.Get();
    }

    public Keep GetById(int keepId)
    {
      Keep foundKeep = _keepsRepository.Get(keepId);
      if (foundKeep == null)
      {
        throw new Exception("Invalid Id");
      }
      _keepsRepository.KeepsViews(keepId);
      foundKeep.Views ++;
      return foundKeep;
    }

    public Keep Post(Keep keepData)
    {
      return _keepsRepository.Create(keepData);
    }
    public void RemoveKeep(int keepId, string userId)
    {
        Keep foundKeep = GetById(keepId);
        if(foundKeep.CreatorId != userId)
        {
            throw new Exception("This is not your keep");
        }
        _keepsRepository.Delete(keepId);
    }
    public Keep Edit(Keep editedKeep)
    {
      Keep foundKeep = GetById(editedKeep.Id);
      if (foundKeep.CreatorId != editedKeep.CreatorId)
      {
        throw new Exception("This is not your keep");
      }
      foundKeep.Name = editedKeep.Name ?? foundKeep.Name;
      foundKeep.Description = editedKeep.Description ?? foundKeep.Description;
      foundKeep.Img = editedKeep.Img ?? foundKeep.Img;
      return _keepsRepository.Edit(foundKeep);
    }
  }
}