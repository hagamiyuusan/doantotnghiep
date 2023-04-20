import { useRef, useState } from 'react'
import styles from './OCR.module.css'

export default function OCR() {
  // const [imgName, setImage] = useState<File | null>(null)
  const [imgName, setImage] = useState<string | ArrayBuffer | null | undefined>(null)
  const inputRef = useRef(null)

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files != null) {
      const reader = new FileReader()
      reader.readAsDataURL(e.target.files[0])
      reader.onload = (readerEvent) => {
        setImage(readerEvent?.target?.result)
      }
      // setImageName(e.target.files[0]) //error
    }
  }
  console.log('Imgae Name', imgName)

  return (
    <div className={`container mx-auto ${styles.ocr} `}>
      <div className={`${styles.title}`}>OCR VIP PRO</div>
      <div className={`${styles.box_ocr} rounded`}>
        <div className='header text-center mt-5'>
          <p className='text-3xl'>Try It Now!</p>
        </div>
        <h1>Title</h1>
        <div className={styles.demo_box}>
          <div
            onClick={() => {
              // eslint-disable-next-line @typescript-eslint/ban-ts-comment
              // @ts-ignore
              inputRef.current.click()
            }}
            className={styles.box_img}
            role='presentation'
          >
            <img src={imgName as string} alt={imgName as string} className='object-cover ob' />
            <div className='box_action text-center flex items-center justify-center'>
              <input
                type='file'
                className='custom-file-input mt-3 text-black bg-cyan-600 '
                onChange={handleChange}
                accept='image/jpeg,image/png,image/webp'
                ref={inputRef}
              />
            </div>
            <div className='text-center flex items-center justify-center mt-3'>
              <button className='bg-cyan-400 m-0 hover:bg-white '>UPLOAD</button>
            </div>
          </div>

          <div className={styles.box_result}>
            {/* ÷ <TabPanel /> */}
            <div className='title mt-4 text-center py-2 border-b border-black'>
              <h1 className='text-xl text-cyan-600'>Description</h1>
            </div>
            <div className='pt-3 px-3 text-lg'>
              <p>There are no recognized results.</p>
              <p>Please check the selected image or model again.</p></div>
          </div>
        </div>
      </div>
    </div>
  )
}
