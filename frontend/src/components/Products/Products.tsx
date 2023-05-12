import { useState,useEffect } from 'react'
import { IProduct } from '~/pages/Profile/AdminPage/ProductManager/ProductManager'
import axios from 'axios'
import './style.css';
import Slider from 'react-slick';
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';

export default function Products() {
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
  };
  
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
  const divs= data?.map(product =>(
  product.durations.map(duration =>(
  <div >
  <div className='grid-cols-1 bg-white rounded shadow-sm pt-4 'style={{ margin: "0px 10px" }} >
    <h1 className=' text-xl text-blue-800 mb-2 text-center'>{product.name}</h1>
    <p className=' text-gray-600 text-center'>Lorem ipsum dolor sit, amet consectetur adipisicingconsectetur</p>
    <p className=' text-5xl text-slate-800 mt-6 text-center' >$ {duration.price}</p>
    <p className=' text-gray-400 mb-6 text-center'>{duration.day} days</p>
    <div className=' px-4'>
      <button className='w-full bg-blue-800 text-white'>Subscrise</button>
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
  </div></div>))))
  return (
    // <div className='grid grid-cols-3 gap-5 text-center mt-20'>
    <div className="slider-container">
        <Slider {...settings} className=''>
        {divs}
        </Slider>
        
     </div>
  )
}
