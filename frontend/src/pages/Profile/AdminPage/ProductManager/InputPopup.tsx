import React, { useState } from 'react'
import ConfirmPopUp from 'src/components/ConfirmPopup/ConfirmPop'

interface IProps {
  setshowInputPrice: React.Dispatch<React.SetStateAction<boolean>>
  setPriceDuration: React.Dispatch<React.SetStateAction<number>>
  addProductDuration: () => Promise<void>
}

export default function InputPopup({ setshowInputPrice, setPriceDuration, addProductDuration }: IProps) {
  const [showConfirmPopup, setShowConfirmPopup] = useState(false)
  return (
    <div className='fixed z-10 inset-0 overflow-y-auto'>
      <div className='flex items-center justify-center min-h-screen'>
        <div className='bg-white rounded-lg shadow-lg p-6 w-[40rem] px-5'>
          <h2 className='text-4xl mb-8 text-center'>Set Price For Duations</h2>
          <div className='flex justify-center items-center'>
            <input
              type='number'
              name='price'
              className='pl-5'
              placeholder='Enter price....'
              onChange={(e) => setPriceDuration(Number(e.target.value))}
            />
          </div>
          <div className='flex justify-end mt-8'>
            <button
              type='button'
              className='bg-blue-500 text-white px-6 py-2 mr-2 rounded'
              onClick={() => setShowConfirmPopup(true)}
            >
              Add
            </button>
            <button
              type='button'
              className='bg-gray-500 text-white px-6 py-2 rounded'
              onClick={() => setshowInputPrice(false)}
            >
              Close
            </button>
          </div>
        </div>
      </div>
      {showConfirmPopup && (
        <ConfirmPopUp
          message='Do you want add duration to product?'
          onOke={addProductDuration}
          onCancel={() => setShowConfirmPopup(false)}
        />
      )}
    </div>
  )
}
