import axios from 'axios'
import { useEffect, useState } from 'react'
import { IDuration } from '../DurationManager/DurationManager'
import InputPopup from './InputPopup'
import { IProduct } from './ProductManager'
import ConfirmPopUp from 'src/components/ConfirmPopup/ConfirmPop'

interface IProps {
  data: IProduct
  setRefreshComponent: React.Dispatch<React.SetStateAction<boolean>>
  setShowAddProductDuration: React.Dispatch<React.SetStateAction<boolean>>
  setShowDetailPopup: React.Dispatch<React.SetStateAction<boolean>>
}

export default function AddProductDurationPopup({
  data: currentDurations,
  setRefreshComponent,
  setShowAddProductDuration,
  setShowDetailPopup
}: IProps) {
  const [durations, setDurations] = useState<IDuration[]>([])
  const [durationId, setDurationId] = useState(0)
  const [showConfirmPopup, setShowConfirmPopup] = useState(false)
  const [showInputPrice, setShowInputPrice] = useState(false)
  const [priceDuration, setPriceDuration] = useState(0)
  const token = localStorage.getItem('access_token') || ''
  // const [refreshComponent, setRefreshComponent] = useState(false);

  const availableDuration = durations.filter(
    (duration) =>
      !currentDurations.durations.some((currentDuration) => currentDuration.original_duration_id === duration.id)
  )

  const getAllDuration = async () => {
    try {
      const res = await axios.get('https://localhost:7749/api/Duration')
      if (res.data.value) {
        setDurations(res.data.value)
      }
    } catch (error) {
      console.log(error)
    }
  }
  const handleClickAdd = (durationId: number) => {
    setDurationId(durationId)
    setShowInputPrice(true)
  }
  const handleOke = async () => {
    try {
      const res = await axios.post(
        `https://localhost:7749/api/ProductDuration`,
        {
          productId: currentDurations.id,
          durationId: durationId,
          price: priceDuration
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            'Content-Type': 'application/json'
          }
        }
      )
      if (res.status === 200) {
        setShowInputPrice(false)
        setDurationId(0)
        setRefreshComponent(false)
        setShowAddProductDuration(false)
        setShowDetailPopup(false)
        // console.log('Respone:', res.data)
      }
    } catch (error) {
      console.log(error)
    }
  }
  useEffect(() => {
    getAllDuration()
  }, [])
  return (
    <div>
      <div className='fixed z-10 inset-0 overflow-y-auto'>
        <div className='flex items-center justify-center min-h-screen'>
          <div className='bg-white rounded-lg shadow-lg p-6 w-[40rem]'>
            <h2 className='text-4xl mb-8 text-center'>Add Duration to Product</h2>
            <div className='flex mb-5'>
              <h3>Current Duration:</h3>
              <ul className='bg-slate-500 rounded-md w-[90%]'>
                {currentDurations?.durations.map((duration, index) => (
                  <li className='flex justify-center items-center gap-5 p-4' key={index}>
                    <span className='w-[175px]'>{duration.day}</span>
                  </li>
                ))}
              </ul>
            </div>

            <div className='flex gap-4 '>
              <label htmlFor='day' className='block  m-0 text-black text-base '>
                Durations Aviable:
              </label>
              <div className='w-full'>
                <ul className='bg-slate-500 rounded-md '>
                  {availableDuration.map((duration, index) => (
                    <li className='flex justify-center items-center gap-5 p-4' key={index}>
                      <span className='w-[175px]'>{duration.day}</span>
                      <button
                        className='font-medium text-blue-600 dark:text-blue-500 hover:underline'
                        onClick={() => handleClickAdd(duration.id)}
                      >
                        Add
                      </button>
                    </li>
                  ))}
                </ul>
              </div>
            </div>
            <div className='flex justify-end mt-8'>
              <button type='button' className='bg-blue-500 text-white px-6 py-2 mr-2 rounded'>
                Edit
              </button>
              <button
                type='button'
                className='bg-gray-500 text-white px-6 py-2 rounded'
                onClick={() => setShowAddProductDuration(false)}
              >
                Close
              </button>
            </div>
          </div>
        </div>
        {showInputPrice && (
          <InputPopup
            setshowInputPrice={setShowInputPrice}
            setPriceDuration={setPriceDuration}
            addProductDuration={handleOke}
          // setDrationID={setDurationId}
          />
        )}
        {showConfirmPopup && <ConfirmPopUp message='Do you want add?' onOke={handleOke} onCancel={() => setShowConfirmPopup(false)} />}
      </div>
    </div>
  )
}
