/* eslint-disable prettier/prettier */
/* eslint-disable @typescript-eslint/no-unused-vars */
import axios from 'axios'
import React, { useEffect, useState } from 'react'
import ConfirmPopUp from 'src/components/ConfirmPopup/ConfirmPop'
import AddProductDurationPopup from './AddProductDurationPopup'
import { IProduct } from './ProductManager'
import { toast } from 'react-toastify'
interface IProps {
  productDetailId: number
  setShowDetailPopup: React.Dispatch<React.SetStateAction<boolean>>
  data?: IProduct
  setRefresh: React.Dispatch<React.SetStateAction<boolean>>
  setProductDetail?: React.Dispatch<React.SetStateAction<IProduct>>
}
// interface IproductDetailForm
export default function TablePopup({
  productDetailId,
  setShowDetailPopup,
  // data,
  setRefresh
}: // setProductDetail
IProps) {
  const [productDetailForm, setproductDetailForm] = useState<IProduct>({
    id: productDetailId,
    name: '',
    apI_URL: ''
  } as IProduct)
  const [showConfirmPopup, setShowConfirmPopup] = useState(false)
  const [showAddProductDuration, setShowAddProductDuration] = useState(false)
  const [durationId, setDurationId] = useState<number>()
  const [messageConfirm, setMessageConfirm] = useState('')
  const token = localStorage.getItem('access_token') || ''

  const handleDelete = async (durationId: number) => {
    setShowConfirmPopup(true)
    setDurationId(durationId)
  }

  const handleOke = async () => {
    try {
      const res = await axios.delete(`${import.meta.env.VITE_BASE_URL}/ProductDuration/${durationId}`, {
        headers: {
          Authorization: `Bearer ${token}`,
          'Content-Type': 'application/json'
        }
      })
      if (res.status === 200) {
        // setProductDetail({} as IProduct)
        toast.error('Delete duration success!', {
          position: toast.POSITION.TOP_RIGHT
        })
        setShowConfirmPopup(false)
        setRefresh(true)
        setShowDetailPopup(false)
      }
    } catch (error) {
      console.log(error)
      toast.error('Delete defective products!', {
        position: toast.POSITION.TOP_RIGHT
      })
      setShowConfirmPopup(false)
      setShowDetailPopup(false)
    }
  }
  const handleCancel = () => {
    setShowConfirmPopup(false)
  }
  const getProductDetail = async () => {
    try {
      const res = await axios.get(`${import.meta.env.VITE_BASE_URL}/Product/${productDetailId}`)
      if (res.status === 200) {
        setproductDetailForm(res.data?.value)
      }
    } catch (error) {
      console.log(error)
    }
  }
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setproductDetailForm((prev) => ({
      ...prev,
      [e.target.name]: e.target.value
    }))
  }
  const handleEdit = async () => {
    try {
      const res = await axios.put(
        `${import.meta.env.VITE_BASE_URL}/Product/${productDetailId}`,
        {
          id: productDetailId,
          name: productDetailForm.name,
          apI_URL: productDetailForm.apI_URL
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            'Content-Type': 'application/json'
          }
        }
      )
      if (res.status === 200) {
        // setProductDetail({} as IProduct)
        toast.success('Edit duration success!', {
          position: toast.POSITION.TOP_RIGHT
        })
        setShowConfirmPopup(false)
        // setRefreshComponent(true)
        setShowDetailPopup(false)
        setRefresh(true)
      }
    } catch (error) {
      toast.error('Delete defective duration!', {
        position: toast.POSITION.TOP_RIGHT
      })
      console.log(error)
      setShowConfirmPopup(false)
    }
  }
  useEffect(() => {
    getProductDetail()
  }, [])
  return (
    <div className='fixed z-10 inset-0 overflow-y-auto'>
      <div className='flex items-center justify-center min-h-screen'>
        <div className='bg-white rounded-lg shadow-lg p-6 w-[40rem]'>
          <h2 className='text-4xl mb-8 text-center'>Product Details</h2>
          <div className='mb-10 flex justify-center items-center gap-4 '>
            <label htmlFor='id' className='block  m-0 text-black text-base w-10'>
              Id:
            </label>
            <input id='id' type='text' className='border-gray-400 p-4  w-9/12' value={productDetailForm?.id} disabled />
          </div>
          <div className='mb-10 flex  justify-center items-center gap-4'>
            <label htmlFor='name' className='block  m-0 text-black text-base w-10'>
              Name:
            </label>
            <input
              id='name'
              name='name'
              type='text'
              className='border-gray-400 p-4 w-9/12 '
              value={productDetailForm.name}
              required
              onChange={handleChange}
            />
          </div>
          <div className='mb-10 flex  justify-center items-center gap-4'>
            <label htmlFor='api_URL' className='block  m-0 text-black text-base w-10 '>
              URL:
            </label>
            <input
              id='apI_URL'
              name='apI_URL'
              type='text'
              className='border-gray-400 p-4 w-9/12 '
              value={productDetailForm.apI_URL}
              required
              onChange={handleChange}
            />
          </div>
          <div className='flex gap-4 '>
            <label htmlFor='day' className='block  m-0 text-black text-base '>
              Durations:
            </label>
            <div className='w-full'>
              <ul className='bg-slate-500 rounded-md w-[90%]'>
                {productDetailForm?.durations?.map((duration, index) => (
                  <li className='flex justify-center items-center gap-5 p-4' key={index}>
                    <span className='w-[175px]'>
                      {duration.day} Day: ${duration.price}{' '}
                    </span>
                    <button
                      className='font-medium text-blue-600 dark:text-blue-500 hover:underline'
                      onClick={() => handleDelete(duration.id)}
                    >
                      Delete
                    </button>
                  </li>
                ))}
                <li>
                  <button
                    className=' bg-sky-600 font-medium text-white w-full  hover:bg-sky-800 hover:text-black'
                    onClick={() => setShowAddProductDuration(true)}
                  >
                    Add Duration
                  </button>
                </li>
              </ul>
            </div>
          </div>
          <div className='flex justify-end mt-8'>
            <button type='button' className='bg-blue-500 text-white px-6 py-2 mr-2 rounded' onClick={handleEdit}>
              Edit
            </button>
            <button
              type='button'
              className='bg-gray-500 text-white px-6 py-2 rounded'
              onClick={() => setShowDetailPopup(false)}
            >
              Close
            </button>
          </div>
        </div>
      </div>
      {showConfirmPopup && <ConfirmPopUp message='Do you want delete?' onOke={handleOke} onCancel={handleCancel} />}
      {showAddProductDuration && (
        <AddProductDurationPopup
          data={productDetailForm}
          setRefreshComponent={setRefresh}
          setShowAddProductDuration={setShowAddProductDuration}
          setShowDetailPopup={setShowDetailPopup}
        />
      )}
    </div>
  )
}
