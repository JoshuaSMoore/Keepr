import { AppState } from "../AppState"
import { Keep } from "../Models/Keep.js"
import { Profile } from "../models/Profile.js"
import { Vault } from "../Models/Vault.js"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService{
  async getUsersProfile(profileId){
    const res = await api.get(`api/profiles/${profileId}`)
    logger.log('users profile res', res)
    AppState.profile = new Profile(res.data)
  }
  async getUsersKeeps(profileId){
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    logger.log('users keeps res', res)
    AppState.keeps = res.data.map(keep => new Keep(keep))
  }
  async getUsersVaults(profileId){
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    logger.log('users vaults res', res)
    AppState.vaults = res.data.map(vault => new Vault(vault))
  }
  async getCurrentUsersVaults(profileId){
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    logger.log('users vaults res', res)
    AppState.usersvaults = res.data.map(vault => new Vault(vault))
  }
}
export const profilesService = new ProfilesService()