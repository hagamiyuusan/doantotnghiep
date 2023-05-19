import React, { useState } from 'react'
import ConfirmPopUp from 'src/components/ConfirmPopup/ConfirmPop'
import { IProduct } from './ProductManager'
import axios from 'axios'
import { toast } from 'react-toastify'

interface IProps {
  setShowAddProductPopup: React.Dispatch<React.SetStateAction<boolean>>
  setRefresh: React.Dispatch<React.SetStateAction<boolean>>
}
const initFormdData: Omit<IProduct, 'durations' | 'id'> = {
  name: '',
  api_URL: ''
}
export default function AddProductPopup({ setShowAddProductPopup, setRefresh }: IProps) {
  const [showConfirmPopup, setShowConfirmPopup] = useState(false)
  const [formData, setFormData] = useState<Omit<IProduct, 'durations' | 'id'>>(initFormdData)
  const token = localStorage.getItem('access_token') || ''

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData((prev) => ({
      ...prev,
      [e.target.name]: e.target.value
    }))
  }
  console.log('FormData', formData)
  const handleOke = async () => {
    try {
      const res = await axios.post(
        `https://localhost:7749/api/Product`,
        {
          typeProductId: 1,
          apI_URL: formData.api_URL,
          name: formData.name,
          created: new Date()
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            'Content-Type': 'application/json'
          }
        }
      )
      if (res.status === 200) {
        toast.success('Add Product Success!', {
          position: toast.POSITION.TOP_RIGHT
        })
        setShowAddProductPopup(false)
        setRefresh(true)
        // setProductDetail({} as IProduct)
        // setShowConfirmPopup(false)
        // setRefresh(true)
        // setShowDetailPopup(false)
      }
    } catch (error) {
      toast.error('Delete defective Product!', {
        position: toast.POSITION.TOP_RIGHT
      })
      console.log(error)
      setShowConfirmPopup(false)
      // setShowDetailPopup(false)
    }
  }
  return (
    <div className='fixed z-10 inset-0 overflow-y-auto'>
      <div className='flex items-center justify-center min-h-screen'>
        <div className='bg-white rounded-lg shadow-lg p-6 w-[40rem]'>
          <h2 className='text-4xl mb-8 text-center'>Add New Product</h2>

          <div className='mb-10 flex  justify-center items-center gap-4'>
            <label htmlFor='name' className='block  m-0 text-black text-base w-10'>
              Name:
            </label>
            <input
              id='name'
              name='name'
              type='text'
              className='border-gray-400 p-4 w-9/12 '
              // value={productDetailForm.name}
              required
              onChange={handleChange}
            />
          </div>
          <div className='mb-10 flex  justify-center items-center gap-4'>
            <label htmlFor='api_URL' className='block  m-0 text-black text-base w-10 '>
              URL:
            </label>
            <input
              id='api_URL'
              name='api_URL'
              type='text'
              className='border-gray-400 p-4 w-9/12 '
              // value={productDetailForm.api_URL}
              required
              onChange={handleChange}
            />
          </div>

          <div className='flex justify-end mt-8'>
            <button type='button' className='bg-blue-500 text-white px-6 py-2 mr-2 rounded' onClick={handleOke}>
              Add
            </button>
            <button
              type='button'
              className='bg-gray-500 text-white px-6 py-2 rounded'
              onClick={() => setShowAddProductPopup(false)}
            >
              Close
            </button>
          </div>
        </div>
      </div>
      {showConfirmPopup && (
        <ConfirmPopUp message='Do you want delete?' onOke={handleOke} onCancel={() => setShowConfirmPopup(false)} />
      )}
    </div>
  )
}
