import axios from 'axios'
import { useEffect, useState } from 'react'
import Table from 'src/components/Table/Table'
import AddProductPopup from './AddProductPopup'
export type IProduct = {
  id: number | undefined
  name: string
  apI_URL: string
  durations: {
    id: number
    day: number
    price: number
    name: string
    original_duration_id: number
  }[]
}

export default function ProductManager() {
  const columnNames = ['Product_Id', 'Name']
  const [data, setData] = useState<IProduct[]>([] as IProduct[])
  const [refesh, setRefresh] = useState(false)
  const [showAddProductPopup, setShowAddProductPopup] = useState(false)
  const getAllProduct = async () => {
    try {
      const res = await axios.get(`${import.meta.env.VITE_BASE_URL}/Product`)
      if (res.data) {
        setData(res.data.value)
        setRefresh(false)
      }
    } catch (error) {
      console.log(error)
    }
  }

  useEffect(() => {
    getAllProduct()
  }, [refesh])
  return (
    <div>
      <div className=' mt-52'>
        <div className='flex justify-center items-center mb-6 '>
          <button
            className='bg-blue-700 text-white px-3 py-4 h-auto hover:bg-gray-600 '
            onClick={() => setShowAddProductPopup(true)}
          >
            Create New Product
          </button>
        </div>
        <Table columnNames={columnNames} title='Product Duration Manager' data={data} setRefresh={setRefresh} />
      </div>
      {showAddProductPopup && (
        <AddProductPopup setShowAddProductPopup={setShowAddProductPopup} setRefresh={setRefresh} />
      )}
    </div>
  )
}
