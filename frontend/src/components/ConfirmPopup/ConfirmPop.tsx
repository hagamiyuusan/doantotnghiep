import React from 'react'
import { IDuration } from '~/pages/Profile/AdminPage/DurationManager/ProductManager'

interface IProps {
  message: string
  onOke: () => Promise<void>
  onCancel?: () => void
  value?: IDuration | undefined
}
export default function ConfirmPopUp({ message, onOke, onCancel, value }: IProps) {
  console.log("🚀 ~ file: ConfirmPop.tsx:11 ~ ConfirmPopUp ~ value:", value)
  const handleOke = () => {
    if (typeof onOke === "function") {
      onOke()
    }
  }
  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center">
      <div className="absolute inset-0 bg-gray-900 opacity-50"></div>
      <div className="relative bg-white p-8 rounded-lg w-80 h-40 flex justify-center items-center flex-col">
        <p className="mb-4">{message}</p>
        <div className="flex justify-end">
          <button
            className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded mr-2"
            onClick={onOke}
          >
            OK
          </button>
          <button
            className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded"
            onClick={onCancel}
          >
            Cancel
          </button>
        </div>
      </div>
    </div>
  )
}
