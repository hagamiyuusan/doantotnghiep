import React from 'react'
import { ProductDuration } from '~/pages/Profile/AdminPage/ProductManager/ProductDurationManager'

interface IProps {
  title: string
  columnNames: string[]
  desc?: string
  data: ProductDuration[]
}

export default function Table({ title, columnNames, data, desc }: IProps) {
  return (
    <div className=' container'>
      <div className='relative overflow-x-auto shadow-md sm:rounded-lg mt-52'>
        <div className='flex justify-center items-center mb-6'>
          {/* <button className='bg-blue-700 text-white px-3 py-4 h-auto hover:bg-gray-600' onClick={handleAddNewDuration}>
            Create New Duration
          </button> */}
        </div>
        <table className='w-full text-sm text-left text-gray-500 dark:text-gray-400'>
          <caption className='p-5 text-lg font-semibold text-left text-gray-900 bg-white dark:text-white dark:bg-gray-800'>
            {title}
            <p className='mt-1 text-sm font-normal text-gray-500 dark:text-gray-400'>{desc}</p>
          </caption>
          <thead className='text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400'>
            <tr>
              {columnNames?.map((column) => (
                <th scope='col' className='px-6 py-3' key={column}>
                  {column}
                </th>
              ))}
            </tr>
          </thead>
          <tbody>
            {data.map((element, index) => (
              <tr className='bg-white border-b dark:bg-gray-800 dark:border-gray-700' key={element.durationId}>
                <th scope='row' className='px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white'>
                  {element.productId}
                </th>
                <td className='px-6 py-4'>{element.price}</td>
                <td className='px-6 py-4 text-right flex gap-6'>
                  <button
                    className='font-medium text-blue-600 dark:text-blue-500 hover:underline'
                  // onClick={() => handleEdit(element)}
                  >
                    Edit
                  </button>
                  <button
                    className='font-medium text-blue-600 dark:text-blue-500 hover:underline'
                  // onClick={() => handleClickDelete(duration)}
                  >
                    {' '}
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
    </div>
  )
}
