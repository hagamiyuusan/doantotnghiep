/* eslint-disable prettier/prettier */
import axios from 'axios'
import { useEffect, useState } from 'react'
import PopupEdit from 'src/components/PopupEdit'
import ConfirmPopUp from 'src/components/ConfirmPopup/ConfirmPop'
export interface IDuration {
  original_duration_id: number
  id: number
  name: string
  day: number
}
const initDuration = {
  id: 0,
  original_duration_id: 0,
  name: '',
  day: 0
}
export default function DuarationManager() {
  const [durations, setDurations] = useState<IDuration[]>([])
  const [duration, setDuration] = useState<IDuration>(initDuration)
  const [showPopupEdit, setShowPopupEdit] = useState(false)
  const [showConfirmPopup, setShowConfirmPopup] = useState(false)
  const token = localStorage.getItem('access_token') || ''

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
  const handleClickDelete = async (duration: IDuration) => {
    // handle Delete
    setDuration(duration)
    setShowConfirmPopup(true)
    console.log('click again');

  }
  const handleOkeDelete = async () => {
    try {
      const res = await axios.delete(`https://localhost:7749/api/Duration/${duration.id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
          'Content-Type': 'application/json'
        }
      });
      if (res.status === 200) {

        setDuration(initDuration)
        setShowConfirmPopup(false)
      }
    } catch (error) {
      console.log(error)
    }
  }
  const handleAddNewDuration = () => {
    setDuration(initDuration)
    setShowPopupEdit(true)
  }
  useEffect(() => {
    getAllDuration()
  }, [duration])
  return (
    <div className=' container'>
      <div className="relative overflow-x-auto shadow-md sm:rounded-lg mt-52">
        <div className="flex justify-center items-center mb-6">
          <button className='bg-blue-700 text-white px-3 py-4 h-auto hover:bg-gray-600' onClick={handleAddNewDuration}>Create New Duration</button>
        </div>
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
                  <button className="font-medium text-blue-600 dark:text-blue-500 hover:underline" onClick={() => handleClickDelete(duration)}> Delete</button>
                </td>
              </tr>

            ))}

          </tbody>
        </table>
      </div>
      {showPopupEdit && <PopupEdit setShowPopupEdit={setShowPopupEdit} duration={duration} setDuration={setDuration} />}
      {showConfirmPopup && <ConfirmPopUp message='Do you want delete?' onOke={handleOkeDelete} value={duration} onCancel={() => setShowConfirmPopup(false)} />}
    </div >
  )
}