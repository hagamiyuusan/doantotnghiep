import jwtDecode from 'jwt-decode'
import { MutableRefObject, createContext, useRef, useState } from 'react'

import { IUser } from 'src/types/user.type'
export interface AppContextInterface {
  isAuthenticated: boolean
  setIsAuthenticated: React.Dispatch<React.SetStateAction<boolean>>
  profile: IUser | null
  setProfile: React.Dispatch<React.SetStateAction<IUser | null>>
  reset: () => void
  ocrRef: MutableRefObject<null | HTMLElement>
}
const getAccessTokenFromLS = () => localStorage.getItem('access_token') || ''
const getProfileFromLS = () => {
  const result = localStorage.getItem('access_token')
  return result ? jwtDecode<IUser>(result) : null
}
// const setProfileToLS = (profile: IUser) => {
//   localStorage.setItem('profile', JSON.stringify(profile))
// }
export const getInitialAppContext: () => AppContextInterface = () => ({
  isAuthenticated: Boolean(getAccessTokenFromLS()),
  setIsAuthenticated: () => null,
  profile: getProfileFromLS(),
  setProfile: () => null,
  reset: () => null,
  ocrRef: { current: null }
})
const initialAppContext = getInitialAppContext()

export const AppContext = createContext<AppContextInterface>(initialAppContext)

export default function AppProvider({
  children,
  defaultValue = initialAppContext
}: {
  children: React.ReactNode
  defaultValue?: AppContextInterface
}) {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(defaultValue.isAuthenticated)
  const [profile, setProfile] = useState<IUser | null>(defaultValue.profile)
  const ocrRef = useRef(null)
  // const ocrElementRef = useRef(null)
  const reset = () => {
    setIsAuthenticated(false)
    setProfile(null)
    localStorage.removeItem('access_token')
  }
  return (
    <AppContext.Provider
      value={{
        isAuthenticated,
        setIsAuthenticated,
        profile,
        setProfile,
        reset,
        ocrRef
      }}
    >
      {children}
    </AppContext.Provider>
  )
}
