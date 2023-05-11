import axios from 'axios'
import React, { useEffect, useState } from 'react'
import Table from 'src/components/Table/Table'

export type IProduct = {
  id: string
  name: string
  durations: { id: number; day: number; price: number }[]
}

export default function ProductManager() {
  const columnNames = ['Product_Id', 'Name']
  const [data, setData] = useState<IProduct[]>([])
  console.log('🚀 ~ file: ProductManager.tsx:14 ~ ProductManager ~ data:', data)
  const getAllProduct = async () => {
    try {
      const res = await axios.get('https://localhost:7749/api/Product')
      if (res.data) {
        setData(res.data.value)
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
      <div className=' mt-52'>
        <div className='flex justify-center items-center mb-6 '>
          <button className='bg-blue-700 text-white px-3 py-4 h-auto hover:bg-gray-600 '>Create New Duration</button>
        </div>
        <Table columnNames={columnNames} title='Product Duration Manager' data={data} />
      </div>
    </div>
  )
}
