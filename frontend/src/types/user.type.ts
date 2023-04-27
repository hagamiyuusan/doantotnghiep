type Role = 'User' | 'Admin'

export interface IUser {
  userName: string
  email?: string
  role?: Role[]
  avatar?: string
  address?: string
  phone?: string
  createdAt: string
  updatedAt: string
}
