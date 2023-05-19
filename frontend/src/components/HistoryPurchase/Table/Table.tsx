import React, { useState } from 'react'
import { toast } from 'react-toastify'
import iconCopy from 'src/imgs/copyIcon.png'
import TablePopup from 'src/pages/Profile/AdminPage/ProductManager/TablePopup'
import { ISubcription } from '../HistoryPurchase'
interface IProps {
  title: string
  columnNames: string[]
  desc?: string
  data: ISubcription[]
  setRefresh: React.Dispatch<React.SetStateAction<boolean>>
}

export default function Table({ title, columnNames, data, desc, setRefresh }: IProps) {
  const convertDate = (stringDate: string) => {
    const date = new Date(stringDate);

    const day = date.getDate()
    const month = date.getMonth() + 1;
    const year = date.getFullYear();

    const formattedDate = `${day < 10 ? '0' + day : day}/${month < 10 ? '0' + month : month}/${year}`;
    return formattedDate

  }
  const copyToClipboard = (text: string) => {
    navigator.clipboard.writeText(text)
      .then(() => {
        console.log('Text copied to clipboard');
      })
      .catch((error) => {
        console.error('Error copying text to clipboard:', error);
      });
  };
  const handleCopyClick = (text: string) => {
    copyToClipboard(text);
    toast.success('Copy Token Success!')
  };
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
            </tr>
          </thead>
          <tbody>
            {data.map((element, index) => (
              <tr className='bg-white border-b dark:bg-gray-800 dark:border-gray-700' key={element.id}>
                <th scope='row' className='px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white'>
                  {element.id}
                </th>
                <th className='px-6 py-4'>{element.productName}</th>
                <th className='px-6 py-4'>{convertDate(element.dueDate)}</th>
                <th className='px-6 py-4' onClick={() => handleCopyClick(element.token)} >
                  <div className="flex justify-center items-center gap-2">
                    {element.token}
                    <img src={iconCopy} alt="iconcopy" className='w-[18px] h-[18px] hover' />
                  </div>
                </th>
                <th className='px-6 py-4'>
                  {element.invoiceViews.map((element, index) => {
                    return (
                      <p key={index} className='mb-1'>
                        {index + 1}:{element.amount}$ {element.isPaid ? 'true' : 'false'}
                      </p>
                    )
                  })}
                </th>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  )
}
