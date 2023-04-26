import React from 'react'
import OCR from '../OCR'
import img_section1 from '../../imgs/section_1.png'
export default function LandingPage() {
  return (
    <div className='container mx-auto  px-8 bg-zinc-900'>
      <section className='pt-8'>
        <h1 className=' uppercase text-4xl text-white text-center mb-8 ' >Image Captioning</h1>
        <h6 className='text-white text-lg text-center'>Explore image storytelling beyond pixels</h6>
        <div className='mt-12  flex justify-center items-center w-full'>
          <img src={img_section1} alt='noimg' className='w-4/5' />
        </div>
        <div className='w-8/12 mx-auto mt-9'>
          <p className='text-white mt-4 text-center'>
            Welcome to SceneXplain, your gateway to revealing the rich narratives hidden within your images. Our cutting-edge AI technology dives deep into every detail, generating sophisticated textual descriptions that breathe life into your visuals. With a user-friendly interface and seamless API integration, SceneXplain empowers developers to effortlessly incorporate our advanced service into their multimodal applications
            <br></br>
            <br></br>
            Bid farewell to uninspired image captions. SceneXplain harnesses the power of state-of-the-art large models and language models to explain the intricate stories beyond the pixels, transcending the limitations of conventional captioning algorithms. Trust in SceneXplain to deliver an engaging, concise, and professional image storytelling experience.
          </p>
        </div>
        <div className='mt-8 flex justify-center items-center gap-10'>
          <button className='text-black bg-white w-48 h-12'>Login To Get Start</button>
          <button className='text-black bg-white w-48 h-12'>Login To Get Start</button>
        </div>
      </section >
      <OCR />
    </div >
  )
}
