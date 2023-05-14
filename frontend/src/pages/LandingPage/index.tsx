import { useRef, useState, useContext } from 'react'
import OCR from '../OCR'
import './styles.css'
import img_section1 from '../../imgs/features-img 1.png'

import { ReviewProduct } from '../../components/ReviewProduct'
import Footer from 'src/components/Footer'
import { AppContext, AppContextInterface } from 'src/Context/context'
import Products from 'src/components/Products/index'
import LoginModal from 'src/components/LoginRegisterModal'

export default function LandingPage() {
  const myVarRef = useRef(0)
  const [showModalLogin, setShowModalLogin] = useState(false)
  const { ocrRef, profile } = useContext(AppContext)
  const scrollToOCR = () => {
    if (ocrRef.current) {
      // Perform the desired action using ocrRef.current
      ocrRef.current.scrollIntoView({ behavior: 'smooth' })
    }
  }
  const handleClick = () => {
    setShowModalLogin(true)
  }
  return (
    <div className='mx-auto'>
      <section className='video-container'>
        <video
          src='https://clova.ai/ocr/res/videos/247728f.mp4'
          muted={true}
          autoPlay={true}
          loop={true}
          height='100%'
          width='100%'
          data-v-35c79de6=''
        ></video>
        <div className='container'>
          <section className='pt-8 h-[650px] bg-gray-50 rounded-lg px-8 mt-[180px]'>
            <div className='flex justify-center items-center gap-7 flex-col md:flex-row'>
              <div className='w-full mx-auto mt-9 text-center'>
                <h1 className='mb-4 text-3xl font-extrabold text-gray-900 dark:text-black md:text-5xl lg:text-6xl'>
                  <span className='text-transparent bg-clip-text bg-gradient-to-r to-emerald-600 from-sky-400'>
                    IMAGE
                  </span>{' '}
                  CAPTIONING
                </h1>
                {/* 
            <h6 className='text-white text-lg text-center'>See the world from a Picture</h6>

            <p className='text-white mt-4  flex-1'>
              Welcome to SceneXplain, your gateway to revealing the rich narratives hidden within your images. Our
              cutting-edge AI technology dives deep into every detail, generating sophisticated textual descriptions
              that breathe life into your visuals. With a user-friendly interface and seamless API integration,
              SceneXplain empowers developers to effortlessly incorporate our advanced service into their multimodal
              applications
            </p> */}
              </div>
              <div className='mst-12 w-full h-[500px] md:w-auto md:h-auto'>
                <img
                  src={img_section1}
                  alt='noimg'
                  className=' h-full object-cover md:max-w-md lg:max-w-lg xl:max-w-xl'
                />
              </div>
            </div>

            <div className='mt-8 flex justify-center items-center gap-10'>
              {!profile?.userName ? (
                <button onClick={handleClick} className='text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800hite w-48 h-12 rounded-md shadow-md hover:shadow-lg focus:outline-none focus:shadow-outline'>
                  Login To Get Start
                </button>
              ) : (
                <button
                  className='inline-flex items-center justify-center px-5 py-3 text-base font-medium text-center text-white bg-blue-700 rounded-lg hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 dark:focus:ring-blue-900'
                  onClick={scrollToOCR}
                >
                  Try it now!
                </button>
              )}
            </div>
          </section>
        </div>
      </section>
      <div className='container'>
        <ReviewProduct />
        <Products />
        <OCR />
      </div>

      {!profile?.userName && showModalLogin && (
        <LoginModal showModalLogin={showModalLogin} setShowModalLogin={setShowModalLogin} />
      )}
    </div>
  )
}
