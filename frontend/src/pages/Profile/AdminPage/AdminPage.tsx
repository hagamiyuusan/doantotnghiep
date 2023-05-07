import axios from 'axios'
import React, { useEffect, useState } from 'react'
import './style.css'
import ManagerUser from './ManagerUser'

interface IInfPage {
  data: []
  errors: null
  firstPage: number
  key: string
  lastPage: number
  message: string
  nextPage: string
  pageNumber: number
  pageSize: number
  previousPage: number
  succeeded: number
  totalPages: number
  totalRecords: number
}
export default function AdminPage() {
  const [users, setUsers] = useState<{ id: string; username: string }[]>([])
  const [infPage, setInfPage] = useState<IInfPage>()
  const [currentPage, setCurrentPage] = useState(1)
  const getAllUser = async () => {
    try {
      const res = await axios.get('https://localhost:7749/api/AppUser', {
        params: {
          pageNumber: currentPage
        }
      })
      if (res.data) {
        setInfPage(res.data)
        setUsers(res.data.data)
      }
    } catch (error) {
      console.log(error)
    }
  }

  useEffect(() => {
    getAllUser()
  }, [currentPage])

  return (
    <ManagerUser
      currentPage={currentPage}
      totalPages={infPage?.totalPages}
      setCurrentPage={setCurrentPage}
      totalRecords={infPage?.totalRecords}
      users={users}
    />
  )
}
