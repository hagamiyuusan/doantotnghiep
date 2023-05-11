import React from 'react'
import { IProduct } from './ProductManager'

interface IProps {
  productDetail: IProduct
}

export default function TablePopup({ productDetail }: IProps) {
  return (
    <div className='fixed z-10 inset-0 overflow-y-auto'>
      <div className='flex items-center justify-center min-h-screen'>
        <div className='bg-white rounded-lg shadow-lg p-6 w-96'>
          <h2 className='text-4xl mb-8 text-center'>Product Details</h2>
          {/* <form> */}
          <div className='mb-10 flex justify-center items-center gap-4 '>
            <label htmlFor='id' className='block  m-0 text-black text-base w-10'>
              Id:
            </label>
            <input
              id='id'
              type='text'
              className='border-gray-400 p-4  w-9/12'
              value={productDetail.id}
              disabled
            // onChange={handleChange}
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
              value={productDetail?.name}
              required
            // onChange={handleChange}
            />
          </div>
          <div className='flex gap-4'>
            <label htmlFor='day' className='block  m-0 text-black text-base '>
              Durations:
            </label>
            <div className="">
              <ul className='bg-slate-500 rounded-md'>
                {productDetail.durations.map((duration, index) => (
                  <li className='flex justify-center items-center gap-5 p-4' key={index}>
                    <span>
                      {duration.day} Day: ${duration.price}{' '}
                    </span>
                    <button className='font-medium text-blue-600 dark:text-blue-500 hover:underline'>Delete</button>
                  </li>
                ))}
                <li>
                  <button className=' bg-sky-600 font-medium text-white w-full  hover:bg-sky-800 hover:text-black'>
                    Add Duration
                  </button>
                </li>
              </ul>
            </div>

          </div>
          <div className='flex justify-end mt-8'>
            <button
              type='button'
              className='bg-blue-500 text-white px-6 py-2 mr-2 rounded'
            // onClick={handleEdit}
            >
              {/* {typeSubmit === TYPESUBMIT.EDIT ? ' Edit' : 'Add'} */}
            </button>
            <button
              type='button'
              className='bg-gray-500 text-white px-6 py-2 rounded'
            // onClick={() => setShowPopupEdit(false)}
            >
              Close
            </button>
          </div>
        </div>
      </div>
      {/* {showConfirmPopup && <ConfirmPopup message='Do you want edit?' onOke={handleOke} onCancel={handleCancel} />} */}
    </div>
  )
}
