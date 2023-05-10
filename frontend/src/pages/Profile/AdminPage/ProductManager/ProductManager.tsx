import axios from 'axios'
import React, { useEffect, useState } from 'react'
import Table from 'src/components/Table/Table'

export interface ProductDuration {
  id: string
  durationId: string
  price: number
  name: string
  //   id	:	number
  // name	:	string
  // apI_URL	:	
}

export default function ProductManager() {
  const columnNames = ['Product_Id', 'Name', 'Price']
  const [data, setData] = useState<ProductDuration[]>([])
  console.log("🚀 ~ file: ProductManager.tsx:14 ~ ProductManager ~ data:", data)
  const getAllProduct = async () => {
    try {
      const res = await axios.get('https://localhost:7749/api/Product')
      if (res.data) {
        setData(res.data.value)
        // setInfPage(res.data)
        // setUsers(res.data.data)
      }
    } catch (error) {
      console.log(error)
    }
  }

  useEffect(() => {
    getAllProduct()
  }, [])
  return (
    <div>
      <Table columnNames={columnNames} title='Product Duration Manager' data={data} />
    </div>
  )
}
