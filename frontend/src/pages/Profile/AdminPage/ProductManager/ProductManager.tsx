/* eslint-disable prettier/prettier */
import axios from 'axios'
import { useEffect, useState } from 'react'
import PopupEdit from 'src/components/PopupEdit'
import ConfirmPopUp from 'src/components/ConfirmPopup/ConfirmPop'
export interface IDuration {
  id: string
  name: string
  day: number
}
export default function ProductManager() {
  const [durations, setDurations] = useState<IDuration[]>([])
  const [duration, setDuration] = useState<IDuration>()
  const [showPopupEdit, setShowPopupEdit] = useState(false)
  const [showConfirmPopup, setShowConfirmPopup] = useState(false)
  const getAllDuration = async () => {
    try {
      const res = await axios.get('https://localhost:7749/api/Duration',)
      if (res.data.value) {
        setDurations(res.data.value)
        // setInfPage(res.data)
        // setUsers(res.data.data)
      }
    } catch (error) {
      console.log(error)
    }
  }
  const handleEdit = (duration: IDuration) => {
    setShowPopupEdit(true)
    setDuration(duration)
  }
  const handleDelete = (duration: IDuration) => {
    // handle Delete
    console.log('Delete duration id: ', duration.id);
  }
  useEffect(() => {
    getAllDuration()
  }, [])
  return (
    <div className=' container'>
      <div className="relative overflow-x-auto shadow-md sm:rounded-lg mt-52">
        <table className="w-full text-sm text-left text-gray-500 dark:text-gray-400">
          <caption className="p-5 text-lg font-semibold text-left text-gray-900 bg-white dark:text-white dark:bg-gray-800">
            Our products
            <p className="mt-1 text-sm font-normal text-gray-500 dark:text-gray-400">Browse a list of Flowbite products designed to help you work and play, stay organized, get answers, keep in touch, grow your business, and more.</p>
          </caption>
          <thead className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
            <tr>
              <th scope="col" className="px-6 py-3">
                ID
              </th>
              <th scope="col" className="px-6 py-3">
                Name
              </th>
              <th scope="col" className="px-6 py-3">
                Day
              </th>
              <th scope="col" className="px-6 py-3">
                <button className="">Edit</button>
              </th>
            </tr>
          </thead>
          <tbody>
            {durations.map((duration, index) => (
              <tr className="bg-white border-b dark:bg-gray-800 dark:border-gray-700" key={duration.id}>
                <th scope="row" className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                  {duration.id}
                </th>
                <td className="px-6 py-4">
                  {duration.name}
                </td>
                <td className="px-6 py-4">
                  {duration.day}
                </td>
                <td className="px-6 py-4 text-right flex gap-6">
                  <button className="font-medium text-blue-600 dark:text-blue-500 hover:underline" onClick={() => handleEdit(duration)}>Edit</button>
                  <button className="font-medium text-blue-600 dark:text-blue-500 hover:underline" onClick={() => handleDelete(duration)}> Delete</button>
                </td>
              </tr>

            ))}

          </tbody>
        </table>
      </div>
      {showPopupEdit && <PopupEdit setShowPopupEdit={setShowPopupEdit} duration={duration} />}
      {/* {showConfirmPopup && <ConfirmPopUp message='Do you want delete?' onOke={handleDelete} />} */}
    </div >
  )
}