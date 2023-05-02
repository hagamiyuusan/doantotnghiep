import { useRef, useState } from 'react'
import styles from './OCR.module.css'
import uploadImgIcon from 'src/imgs/add_photo_alternate_outlined.png'

export default function OCR() {
  const [imgName, setImage] = useState<string | ArrayBuffer | null | undefined>(null)
  const inputRef = useRef(null)
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files != null) {
      const reader = new FileReader()
      reader.readAsDataURL(e.target.files[0])
      reader.onload = (readerEvent) => {
        setImage(readerEvent?.target?.result)
      }
    }
  }
  console.log('Imgae Name', imgName)

  return (
    <div className={`container mx-auto bg-zinc-900 ${styles.ocr} pb-40`}>
      <h1
        className='uppercase text-4xl  text-center mt-8 
              bg-gradient-to-r from-blue-600 via-green-500 to-indigo-400 inline-block text-transparent bg-clip-text'
      >
        Image Captioning
      </h1>
      <div className='rounded '>
        <div className='header text-center mt-5 mb-16'>
          <p className='text-3xl text-white'>Try It Now!</p>
        </div>

        <div className='flex justify-center items-center gap-5 '>
          <div className=''>
            <div
              onClick={() => {
                // eslint-disable-next-line @typescript-eslint/ban-ts-comment
                // @ts-ignore
                inputRef.current.click()
              }}
              className='w-[450px] h-[450px] bg-gray-400 flex items-center justify-center rounded-sm'
              role='presentation'
            >
              {imgName ? (
                <img
                  src={imgName as string}
                  alt={imgName as string}
                  className='object-cover w-full h-full object-center'
                />
              ) : (
                <img
                  // src={imgName as string}
                  src={uploadImgIcon}
                  alt={imgName as string}
                  className='object-cover w-[100px] h-[100px] object-center'
                />
              )}
              <div className='box_action text-center flex items-center justify-center'>
                <input
                  type='file'
                  className='custom-file-input mt-3 text-black bg-cyan-600 '
                  onChange={handleChange}
                  accept='image/jpeg,image/png,image/webp'
                  ref={inputRef}
                  hidden
                />
              </div>
            </div>
            <div className='text-center flex items-center justify-center mt-3'>
              <button className='bg-cyan-400 m-0 hover:bg-white w-full'>UPLOAD</button>
            </div>
          </div>
          <div className='w-[450px] h-[450px]'>
            {/* ÷ <TabPanel /> */}
            <div className='title  text-center  border-b border-white'>
              <h1 className='text-xl text-cyan-600 uppercase -translate-y-6'>Description</h1>
            </div>
            <div className='pt-3 px-3 text-lg text-white'>
              <p>There are no recognized results.</p>
              <p>Please check the selected image or model again.</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}

