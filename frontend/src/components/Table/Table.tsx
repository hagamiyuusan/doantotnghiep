import React, { useContext, useState } from 'react'
import { IProduct } from '~/pages/Profile/AdminPage/ProductManager/ProductManager'
import TablePopup from 'src/pages/Profile/AdminPage/ProductManager/TablePopup'
import axios from 'axios'
import ConfirmPopUp from '../ConfirmPopup/ConfirmPop'

interface IProps {
  title: string
  columnNames: string[]
  desc?: string
  data: IProduct[]
  setRefresh: React.Dispatch<React.SetStateAction<boolean>>
}

export default function Table({ title, columnNames, data, desc, setRefresh }: IProps) {
  const [showDetailPopup, setShowDetailPopup] = useState(false)
  const [showConfirmPopup, setShowConfirmPopup] = useState(false)
  const [productDetailId, setProductDetailId] = useState<number>(0)
  const [productDeleteId, setProductDeleteId] = useState(0)
  const token = localStorage.getItem('access_token') || ''

  const handleDetailClick = (element: number) => {
    setShowDetailPopup(true)
    setProductDetailId(element)
  }
  const handleClickDelete = async (id: number) => {
    setProductDeleteId(id)
    setShowConfirmPopup(true)
  }
  const handleDeleteById = async () => {
    try {
      const res = await axios.delete(
        `https://localhost:7749/api/Product/${productDeleteId}`,

        {
          headers: {
            Authorization: `Bearer ${token}`,
            'Content-Type': 'application/json'
          }
        }
      )
      if (res.status === 200) {
        console.log('Respone:', res.data)
        setRefresh(true)
        // setProductDetail({} as IProduct)
        setShowConfirmPopup(false)
        // setRefreshComponent(true)
        // setShowDetailPopup(false)
      }
    } catch (error) {
      console.log(error)
      // setShowConfirmPopup(false)
    }
  }
  return (
    <div className=' container'>
      <div className='relative overflow-x-auto shadow-md sm:rounded-lg'>
        <div className='flex justify-center items-center mb-6'></div>
        <table className='w-full text-sm text-left text-gray-500 dark:text-gray-400'>
          <caption className='p-5 text-lg font-semibold text-left text-gray-900 bg-white dark:text-white dark:bg-gray-800'>
            {title}
            <p className='mt-1 text-sm font-normal text-gray-500 dark:text-gray-400'>{desc}</p>
          </caption>
          <thead className='text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400'>
            <tr>
              {columnNames?.map((column) => (
                <>
                  <th scope='col' className='px-6 py-3' key={column}>
                    {column}
                  </th>
                </>
              ))}
              <th scope='col' className='px-6 py-3'></th>
            </tr>
          </thead>
          <tbody>
            {data.map((element, index) => (
              <tr className='bg-white border-b dark:bg-gray-800 dark:border-gray-700' key={element.id}>
                <th scope='row' className='px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white'>
                  {element.id}
                </th>
                <td className='px-6 py-4'>{element.name}</td>
                <td className='px-6 py-4 text-right flex gap-6'>
                  <button
                    className='font-medium text-blue-600 dark:text-blue-500 hover:underline'
                    onClick={() => handleDetailClick(element.id as number)}
                  >
                    Details
                  </button>
                  <button
                    className='font-medium text-blue-600 dark:text-blue-500 hover:underline'
                    onClick={() => handleClickDelete(element.id as number)}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      {/* {showPopupEdit && <PopupEdit setShowPopupEdit={setShowPopupEdit} duration={duration} setDuration={setDuration} />} */}
      {/* {showConfirmPopup && (
        <ConfirmPopUp
          message='Do you want delete?'
          onOke={handleOkeDelete}
          value={duration}
          onCancel={() => setShowConfirmPopup(false)}
        />
      )} */}
      {showDetailPopup && (
        <TablePopup productDetailId={productDetailId} setRefresh={setRefresh} setShowDetailPopup={setShowDetailPopup} />
      )}
      {showConfirmPopup && (
        <ConfirmPopUp
          message='Do you want delete product?'
          onOke={handleDeleteById}
          onCancel={() => setShowConfirmPopup(false)}
        />
      )}
    </div>
  )
}
