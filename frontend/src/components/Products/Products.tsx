<<<<<<< Updated upstream
import { useState, useEffect, useContext } from 'react'
=======
import { useState, useEffect } from 'react'
>>>>>>> Stashed changes
import { IProduct } from '~/pages/Profile/AdminPage/ProductManager/ProductManager'
import axios from 'axios'
import './style.css'
import Slider from 'react-slick'
import 'slick-carousel/slick/slick.css'
import 'slick-carousel/slick/slick-theme.css'
<<<<<<< Updated upstream
import { AppContext, AppContextInterface } from 'src/Context/context'
import LoginModal from 'src/components/LoginRegisterModal'


=======
>>>>>>> Stashed changes

export default function Products() {
  const [isLoading, setIsLoading] = useState(false)
  const [showModalLogin, setShowModalLogin] = useState(false)
  const { ocrRef, profile } = useContext(AppContext)

  const openInNewTab = (url: string | URL | undefined) => {
    window.open(url, '_blank', 'noopener,noreferrer')
  }

  const [data, setData] = useState<IProduct[]>([])
  const settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 3,
    slidesToScroll: 1,
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 1,
          infinite: true,
          dots: true
        }
      },
      {
        breakpoint: 600,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          initialSlide: 1
        }
      }
    ]
  }

<<<<<<< Updated upstream
  const payload = async (id: number, username: string) => {
    try {
      setIsLoading(true)
      const res = await axios.post('https://localhost:7749/api/Subscription/createpayment', {
        productDurationId: id,
        username: username
      })
      setIsLoading(false)

      if (res.data) {
        openInNewTab(res.data)
      }
    } catch (error) {
      console.log(error)
    }
  }

=======
>>>>>>> Stashed changes
  const getAllProduct = async () => {
    try {
      const res = await axios.get('https://localhost:7749/api/Product')
      if (res.data) {
        setData(res.data.value)
        console.log(res)
      }
    } catch (error) {
      console.log(error)
    }
  }
  useEffect(() => {
    getAllProduct()
  }, [])
  const divs = data?.map((product) =>
    product.durations.map((duration) => (
<<<<<<< Updated upstream
      <div>
=======
      <div key={duration.id}>
>>>>>>> Stashed changes
        <div className='grid-cols-1 bg-white rounded shadow-sm pt-4 ' style={{ margin: '0px 10px' }}>
          <h1 className=' text-xl text-blue-800 mb-2 text-center'>{product.name}</h1>
          <p className=' text-gray-600 text-center'>Lorem ipsum dolor sit, amet consectetur adipisicingconsectetur</p>
          <p className=' text-5xl text-slate-800 mt-6 text-center'>$ {duration.price}</p>
          <p className=' text-gray-400 mb-6 text-center'>{duration.day} days</p>
          <div className=' px-4'>
<<<<<<< Updated upstream
            <button
              onClick={
                profile?.userName
                  ? () => payload(duration.original_duration_id, profile?.userName)
                  : () => setShowModalLogin(true)
              }
              type='button'
              className={
                isLoading
                  ? 'w-full text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center mr-2 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800 inline-flex items-center'
                  : 'w-full text-gray-900 bg-[#F7BE38] hover:bg-[#F7BE38]/90 focus:ring-4 focus:outline-none focus:ring-[#F7BE38]/50 font-medium rounded-lg text-sm px-5 py-2.5 text-center inline-flex items-center dark:focus:ring-[#F7BE38]/50 mr-2 mb-2'
              }
            >
              <svg
                className='w-4 h-4 mr-2 -ml-1'
                aria-hidden='true'
                focusable='false'
                data-prefix='fab'
                data-icon='paypal'
                role='img'
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 384 512'
              >
                {isLoading ? (
                  <path
                    d='M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z'
                    fill='#currenColor'
                  />
                ) : (
                  <path
                    fill='E5E7EB'
                    d='M111.4 295.9c-3.5 19.2-17.4 108.7-21.5 134-.3 1.8-1 2.5-3 2.5H12.3c-7.6 0-13.1-6.6-12.1-13.9L58.8 46.6c1.5-9.6 10.1-16.9 20-16.9 152.3 0 165.1-3.7 204 11.4 60.1 23.3 65.6 79.5 44 140.3-21.5 62.6-72.5 89.5-140.1 90.3-43.4 .7-69.5-7-75.3 24.2zM357.1 152c-1.8-1.3-2.5-1.8-3 1.3-2 11.4-5.1 22.5-8.8 33.6-39.9 113.8-150.5 103.9-204.5 103.9-6.1 0-10.1 3.3-10.9 9.4-22.6 140.4-27.1 169.7-27.1 169.7-1 7.1 3.5 12.9 10.6 12.9h63.5c8.6 0 15.7-6.3 17.4-14.9 .7-5.4-1.1 6.1 14.4-91.3 4.6-22 14.3-19.7 29.3-19.7 71 0 126.4-28.8 142.9-112.3 6.5-34.8 4.6-71.4-23.8-92.6z'
                  ></path>
                )}
              </svg>
              Check out with PayPal
            </button>
=======
            <button className='w-full bg-blue-800 text-white'>Subscrise</button>
>>>>>>> Stashed changes
          </div>
          <div className='pb-8 mt-6'>
            <div className='flex items-center gap-3 px-4 mb-3'>
              {/* <i className="">icon</i> */}
              <i className='gg-check-o text-blue-800 bg-blue-100'></i>
              <p>Testtttttt</p>
            </div>

            <div className='flex items-center gap-3 px-4 mb-3'>
              {/* <i className="">icon</i> */}
              <i className='gg-check-o text-blue-800 bg-blue-100'></i>
              <p>Testtttttt</p>
            </div>

            <div className='flex items-center gap-3 px-4 mb-3'>
              {/* <i className="">icon</i> */}
              <i className='gg-check-o text-blue-800 bg-blue-100'></i>
              <p>Testtttttt</p>
            </div>
          </div>
        </div>
      </div>
    ))
  )
  return (
    // <div className='grid grid-cols-3 gap-5 text-center mt-20'>
    <div className='slider-container'>
      <Slider {...settings} className=''>
        {divs}
      </Slider>
<<<<<<< Updated upstream

      {!profile?.userName && showModalLogin && (
        <LoginModal showModalLogin={showModalLogin} setShowModalLogin={setShowModalLogin} />
      )}
=======
>>>>>>> Stashed changes
    </div>
  )
}
