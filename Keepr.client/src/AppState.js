import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  keeps : [],
  keep: null,
  vaults: [],
  vault: null,
  vaultkeeps: [],
  vaultkeep: null,
  usersvaults: [],
})
