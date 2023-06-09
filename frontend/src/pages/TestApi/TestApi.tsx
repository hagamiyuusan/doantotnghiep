import { useRef, useState, ChangeEvent, useContext } from 'react'
import styles from './TestApi.module.css'
import uploadImgIcon from 'src/imgs/add_photo_alternate_outlined.png'
import axios from 'axios'
import { AppContext } from 'src/Context/context'

interface IForm {
  idProduct: number
  token: string
}

export default function TestApi() {
  const [formData, setFormData] = useState<IForm>({
    idProduct: 1,
    token: ''
  })
  const pRef = useRef<HTMLParagraphElement>(null)
  const inputRef = useRef(null)
  const [loading, setLoading] = useState(false)
  const [selectedFile, setSelectedFile] = useState<File | null>(null)
  const [caption, setCaption] = useState('')
  const { ocrRef } = useContext(AppContext)
  const onChangeHandler = (event: ChangeEvent<HTMLInputElement>) => {
    setSelectedFile(event.target.files ? event.target.files[0] : null)
    setCaption('')
  }
  const handleCopy = () => {
    if (pRef.current) {
      const range = document.createRange()
      range.selectNodeContents(pRef.current)

      const selection = window.getSelection()
      if (selection) {
        selection.removeAllRanges()
        selection.addRange(range)
      }

      document.execCommand('copy')
    }
  }
  const onClickHandler = () => {
    if (!selectedFile) return
    setLoading(true)
    const data = new FormData()
    data.append('image', selectedFile)
    data.append('idProduct', '1')
    data.append('token', formData.token)
    axios
      .post<{ result: string, code : number }>(`${import.meta.env.VITE_BASE_URL}/Product/subscription`, data, {})
      .then((res) => {
        setCaption(res.data.result)
        setLoading(false)
      })
      .catch((err) => {
        console.log(err)
        setLoading(false)
      })
  }
  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setFormData((prev) => ({ ...prev, [e.target.name]: e.target.value }))
  }
  console.log(formData);

  return (
    <div
      className={`container mx-auto bg-gray-50 rounded-lg ${styles.ocr} pb-40`}
      ref={ocrRef as React.LegacyRef<HTMLDivElement>}
    >
      {/* <h1 className='uppercase text-4xl text-black font-bold	'>Image Captioning</h1> */}
      <form action=''>
        <div className='rounded '>
          <div className='header text-center mt-5 mb-16'>
            <p className='text-3xl text-black'>Try It Now!</p>
          </div>
        
          <div className=' mb-16 flex flex-col justify-stretch w-90%'>
            {/* <div className='w-[920px] mx-auto mb-[12px] h-[32px]'>
              <input className='pl-[12px]' type='text' placeholder='Enter Id Product...' required onChange={handleChange} name='idProduct' />
            </div> */}
            <div className='w-[920px] mx-auto h-[32px]'>
              <input className='pl-[12px]' type='text' placeholder='Enter Your Token...' required onChange={handleChange} name='token' />
            </div>
          </div>
        
           <div className='flex justify-center gap-5 items-stretch'>
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
                {selectedFile ? (
                  <img
                    // eslint-disable-next-line @typescript-eslint/ban-ts-comment
                    // @ts-ignore
                    src={selectedFile && URL.createObjectURL(selectedFile)}
                    alt={selectedFile + ''}
                    className='object-cover w-full h-full object-center'
                  />
                ) : (
                  <img
                    // src={selectedFile as string}
                    src={uploadImgIcon}
                    alt={selectedFile + ''}
                    className='object-cover w-[100px] h-[100px] object-center'
                  />
                )}
                <div className='box_action text-center flex items-center justify-center'>
                  <input
                    type='file'
                    className='custom-file-input mt-3 text-black bg-cyan-600 '
                    onChange={onChangeHandler}
                    accept='image/jpeg,image/png,image/webp'
                    ref={inputRef}
                    hidden
                  />
                </div>
              </div>
              <div className='text-center flex items-center justify-center mt-3'>
                <button
                  onClick={onClickHandler}
                  className='w-full bg-blue-800 text-white flex items-center justify-center gap-3'
                  disabled={!loading ? false : true}
                >
                  UPLOAD YOUR IMAGE
                  {loading && (
                    <div role='status'>
                      <svg
                        aria-hidden='true'
                        className='w-6 h-6 mr-2 text-gray-200 animate-spin dark:text-gray-600 fill-blue-600'
                        viewBox='0 0 100 101'
                        fill='none'
                        xmlns='http://www.w3.org/2000/svg'
                      >
                        <path
                          d='M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z'
                          fill='currentColor'
                        />
                        <path
                          d='M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z'
                          fill='currentFill'
                        />
                      </svg>
                    </div>
                  )}
                </button>
              </div>
            </div>
            <div className='w-[450px] h-[450px] place-items-stretch'>
              {/* ÷ <TabPanel /> */}
              {/* <button   style={{color:'red',float:'right'}}>Copy</button> */}
              {/* <button onClick={handleCopy} style={{color:'red',float:'right'}} className="flex ml-auto gap-2"><svg stroke="currentColor" fill="none" strokeLidth="2" viewBox="0 0 24 24" strokeLinecap="round" strokeLinejoin="round" className="h-4 w-4" height="1em" width="1em" xmlns="http://www.w3.org/2000/svg"><path d="M16 4h2a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2H6a2 2 0 0 1-2-2V6a2 2 0 0 1 2-2h2"></path><rect x="8" y="2" width="8" height="4" rx="1" ry="1"></rect></svg>Copy</button>
            <div className='title  text-center  border-b border-white'>
              <h1 className='text-xl text-cyan-600 uppercase -translate-y-6'>Description</h1>
            </div> */}
              <div className='flex items-center relative text-gray-200 bg-gray-800 px-4 py-2 text-xs font-sans justify-between rounded-t-md '>
                <span>Description</span>
                <button onClick={handleCopy} className='flex ml-auto gap-2'>
                  <svg
                    stroke='currentColor'
                    fill='none'
                    strokeWidth='2'
                    viewBox='0 0 24 24'
                    strokeLinecap='round'
                    strokeLinejoin='round'
                    className='h-4 w-4'
                    height='1em'
                    width='1em'
                    xmlns='http://www.w3.org/2000/svg'
                  >
                    <path d='M16 4h2a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2H6a2 2 0 0 1-2-2V6a2 2 0 0 1 2-2h2'></path>
                    <rect x='8' y='2' width='8' height='4' rx='1' ry='1'></rect>
                  </svg>
                  Copy text
                </button>
              </div>

              {/* <div className='pt-3 px-3 text-lg text-white' > */}
              <div className='p-4 overflow-y-auto bg-gray-500 h-full '>
                {!caption ? (
                  <>
                    <p className='  text-gray-200'>There are no recognized results.</p>
                    <p className='  text-gray-200'>Please check the selected image or model again.</p>
                  </>
                ) : (
                  <>
                    <p className='  text-gray-200' ref={pRef}>
                      {caption}
                    </p>
                  </>
                )}
              </div>
            </div>
          </div>
        </div>
      </form>
    </div>
  )
}
