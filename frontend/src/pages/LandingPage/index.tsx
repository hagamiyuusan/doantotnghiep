
import OCR from '../OCR'
import img_section1 from '../../imgs/anhnen.png'
import { ReviewProduct } from '../../components/ReviewProduct'
import Footer from 'src/components/Footer'

import Products from 'src/components/Products/index'
export default function LandingPage() {
  return (
    <div className='container mx-auto'>
      <section className='pt-8 h-[800px] bg-zinc-900 px-8 rounded-sm mt-28 rounded-sm'>
        <div className='flex justify-center items-center gap-7'>
          <div className='w-full mx-auto mt-9 text-center'>
            <h1
              className='uppercase text-4xl  text-center mb-8 
              bg-gradient-to-r from-blue-600 via-green-500 to-indigo-400 inline-block text-transparent bg-clip-text'
            >
              Image Captioning
            </h1>
            <h6 className='text-white text-lg text-center'>See the world from a Picture</h6>

            <p className='text-white mt-4  flex-1'>
              Welcome to SceneXplain, your gateway to revealing the rich narratives hidden within your images. Our
              cutting-edge AI technology dives deep into every detail, generating sophisticated textual descriptions
              that breathe life into your visuals. With a user-friendly interface and seamless API integration,
              SceneXplain empowers developers to effortlessly incorporate our advanced service into their multimodal
              applications
            </p>
          </div>
          <div className='mst-12 w-full h-[500px]'>
            <img src={img_section1} alt='noimg' className=' h-full' />
          </div>
        </div>

        <div className='mt-8 flex justify-center items-center gap-10'>
          <button className='text-black bg-white w-48 h-12'>Login To Get Start</button>
        </div>
      </section>
      <ReviewProduct />
      <Products />
      <OCR />
      <Footer/>

    </div>
  )
}