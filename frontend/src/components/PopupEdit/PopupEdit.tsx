import React, { useState } from 'react'
import { IDuration } from 'src/pages/Profile/AdminPage/ProductManager/ProductManager'
import ConfirmPopup from '../ConfirmPopup'
import axios from 'axios'

interface IProps {
  setShowPopupEdit: React.Dispatch<React.SetStateAction<boolean>>
  duration?: IDuration
}
const initFormEdit = {
  id: '',
  name: '',
  day: 0
}
const PopupEdit: React.FC<IProps> = ({ setShowPopupEdit, duration }) => {
  const [formEdit, setFormEdit] = useState<IDuration>(duration as IDuration)
  const [showConfirmPopup, setShowConfirmPopup] = useState(false)
  const token = localStorage.getItem('access_token') || ''
  console.log(token)

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormEdit((prev) => {
      return { ...prev, [e.target.name]: e.target.value }
    })
  }
  const handleEdit = () => {
    // Do something with the input values
    setShowConfirmPopup(true)
  }
  const handleOke = async () => {
    try {
      const res = await axios.put(
        `https://localhost:7749/api/Duration`,
        {
          id: formEdit?.id,
          name: formEdit?.name,
          day: formEdit?.day
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            'Content-Type': 'application/json'
          }
        }
      )
      console.log(res)
    } catch (error) {
      console.log(error)
    }

    console.log('okeeee')
  }
  const handleCancel = () => {
    setShowConfirmPopup(false)
  }
  console.log('duration', duration)

  return (
    <div className='fixed z-10 inset-0 overflow-y-auto'>
      <div className='flex items-center justify-center min-h-screen'>
        <div className='bg-white rounded-lg shadow-lg p-6 w-96'>
          <h2 className='text-4xl mb-8 text-center'>Edit Item</h2>
          <form>
            <div className='mb-10 flex justify-center items-center gap-4 '>
              <label htmlFor='id' className='block  m-0 text-black text-base w-10'>
                Id:
              </label>
              <input
                id='id'
                type='text'
                className='border-gray-400 p-4  w-9/12'
                value={formEdit.id}
                disabled
                onChange={handleChange}
              />
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
                value={formEdit.name}
                onChange={handleChange}
              />
            </div>
            <div className='mb-10 flex  justify-center items-center gap-4'>
              <label htmlFor='day' className='block  m-0 text-black text-base w-10'>
                Day:
              </label>
              <input
                id='day'
                name='day'
                type='text'
                className='border-gray-400 p-4 w-9/12'
                value={formEdit.day}
                onChange={handleChange}
              />
            </div>
            <div className='flex justify-end mt-8'>
              <button type='button' className='bg-blue-500 text-white px-6 py-2 mr-2 rounded' onClick={handleEdit}>
                Edit
              </button>
              <button
                type='button'
                className='bg-gray-500 text-white px-6 py-2 rounded'
                onClick={() => setShowPopupEdit(false)}
              >
                Close
              </button>
            </div>
          </form>
        </div>
      </div>
      {showConfirmPopup && <ConfirmPopup message='Do you want edit?' onOke={handleOke} onCancel={handleCancel} />}
    </div>
  )
}

export default PopupEdit
