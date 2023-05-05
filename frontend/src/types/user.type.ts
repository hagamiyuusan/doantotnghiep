type Role = 'User' | 'Admin'

export interface IUser {
  userName: string
  email?: string
  role?: string
  avatar?: string
  address?: string
  phone?: string
  createdAt: string
  updatedAt: string
}
