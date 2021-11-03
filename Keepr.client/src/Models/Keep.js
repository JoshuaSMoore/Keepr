export class Keep{
  constructor(data = {})
    {
    this.id = data.id
    this.creatorId = data.creatorId
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.name = data.name
    this.description = data.description
    this.views = data.views
    this.img = data.img
    this.shares = data.shares
    this.keeps = data.keeps
    this.creator = data.creator || {}
    this.vaultKeepId = data.vaultKeepId
    }
}