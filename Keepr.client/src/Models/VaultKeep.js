export class VaultKeep {
  constructor(data = {}){
    this.id = data.id;
    this.creatorId = data.creatorId;
    this.createdAt = data.createdAt;
    this.updatedAt = data.updatedAt;
    this.creator = data.creator || {};
    this.keepId = data.keepId;
    this.vaultId = data.vaultId;
  }
}