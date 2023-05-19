import axios from 'axios'
import { useContext, useEffect, useState } from 'react'
import { AppContext } from 'src/Context/context'
import Table from './Table/Table'
export interface ISubcription {
  id: number
  dueDate: string
  productName: string
  token: string
  invoiceViews: {
    amount: number
    isPaid: string
  }[]
}

export default function HistoryPurchase() {
  const columnNames = ['Id', 'Product Name', 'Due-Day', 'Token', 'History Invoice']
  const [data, setData] = useState<ISubcription[]>([])
  const [refresh, setRefresh] = useState(false)
  const { profile } = useContext(AppContext)
  // const tableData = data.map((item, index) => {

  // });
  const getSubcription = async () => {
    try {
      const res = await axios.get(`https://localhost:7749/api/Subscription/user/${profile?.userName}`)
      if (res.status === 200) {
        console.log('Respone:', res.data.data.value.data)
        setData(res.data.data.value.data)
      }
    } catch (error) {
      console.log(error)

    }
  }
  useEffect(() => {
    getSubcription()
  }, [])
  return (
    <div className='mt-[150px]'>
      <h1 className='text-center text-5xl mb-[60px]'>History Purcharse</h1>
      <Table columnNames={columnNames} title='History Purcharse' data={data} setRefresh={setRefresh} />

    </div>
  )
}
