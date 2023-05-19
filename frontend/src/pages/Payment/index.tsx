import { useRef, useState, ChangeEvent, useContext, useEffect } from 'react'
import axios from 'axios'
import { useLocation, useNavigate, useParams } from 'react-router-dom'

export default function Payment() {
  const [payment, setPayment] = useState(false)
  const location = useLocation()
  const currentPath = location.pathname
  const searchParams = new URLSearchParams(location.search)
  const paymentId = searchParams.get('paymentId')
  const token = searchParams.get('token')
  useEffect(() => {
    const getPayment = async () => {
      try {
        const res = await axios.post(`https://localhost:7749/api/Subscription/payment`, {
          paymentId: paymentId,
          token: token
        })
        if (res.status === 200) {
          console.log('Respone:', res.data)
          setPayment(true)
        }
      } catch (error) {
        console.log(error)
      }
    }
    getPayment()
  }, [])
  return (
    <div className='bg-gray-100 h-screen'>
      <div className='bg-white p-6  md:mx-auto'>
        {payment ? (
          <>
            <svg viewBox='0 0 24 24' className='text-green-600 w-16 h-16 mx-auto my-6'>
              <path
                fill='currentColor'
                d='M12,0A12,12,0,1,0,24,12,12.014,12.014,0,0,0,12,0Zm6.927,8.2-6.845,9.289a1.011,1.011,0,0,1-1.43.188L5.764,13.769a1,1,0,1,1,1.25-1.562l4.076,3.261,6.227-8.451A1,1,0,1,1,18.927,8.2Z'
              ></path>
            </svg>
            <div className='text-center'>
              <h3 className='md:text-2xl text-base text-gray-900 font-semibold text-center'>Payment Done!</h3>
              <p className='text-gray-600 my-2'>Thank you for completing your secure online payment.</p>
              <p> Have a great day! </p>
              <div className='py-10 text-center'>
                <a href='#!' className='px-12 bg-indigo-600 hover:bg-indigo-500 text-white font-semibold py-3'>
                  GO BACK
                </a>
              </div>
            </div>
          </>
        ) : (
          <>
            <svg viewBox='0 0 24 24' className='text-green-600 w-16 h-16 mx-auto my-6'>
              <path
                fill='red'
                d='M12 4a8 8 0 1 0 0 16 8 8 0 0 0 0-16zM2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12zm5.793-4.207a1 1 0 0 1 1.414 0L12 10.586l2.793-2.793a1 1 0 1 1 1.414 1.414L13.414 12l2.793 2.793a1 1 0 0 1-1.414 1.414L12 13.414l-2.793 2.793a1 1 0 0 1-1.414-1.414L10.586 12 7.793 9.207a1 1 0 0 1 0-1.414z'
              ></path>
            </svg>
            <div className='text-center'>
              <h3 className='md:text-2xl text-base text-gray-900 font-semibold text-center'>Payment Failed!</h3>
              <div className='py-10 text-center'>
                <a href='#!' className='px-12 bg-indigo-600 hover:bg-indigo-500 text-white font-semibold py-3'>
                  GO BACK
                </a>
              </div>
            </div>
          </>
        )}
      </div>
    </div>
  )
}
