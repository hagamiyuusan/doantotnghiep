import axios from 'axios'
import React, { useState } from 'react'
import { useLocation, useParams } from 'react-router-dom'
import { ErrorMessage } from 'src/components/ErrorMessage'

interface IFormData {
  email: string
  newPassword: string
}
const initFormData = {
  email: '',
  newPassword: ''
}
enum TYPESUBMIT {
  SENDMAIL = 'SENDMAIL',
  RESET = 'RESET'
}
enum TYPEFORM {
  SENDMAIL = '/sendMailChangePassword',
  RESET = '/sendMailChangePassword/:username/:token'
}
export default function ChangePassword() {
  const [formData, setFormData] = useState<IFormData>(initFormData)
  const [loading, setLoading] = useState<boolean>(false)
  const location = useLocation()
  const currentPath = location.pathname
  const typeForm = currentPath === TYPEFORM.SENDMAIL ? TYPESUBMIT.SENDMAIL : TYPESUBMIT.RESET
  const { username, token } = useParams()
  const [message, setMessage] = useState('')
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData((prev) => ({ ...prev, [e.target.name]: e.target.value }))
  }
  const handleSubmit = (typeSubmit: string) => async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    if (typeSubmit === TYPESUBMIT.SENDMAIL) {
      setLoading(true)
      try {
        const res = await axios.post(`${import.meta.env.VITE_BASE_URL}/UserService/password/reset`, {
          email: formData.email
        })
        if (res.status === 200) {
          setMessage('Check your mail to change password')
          window.open('https://www.gmail.com')
          setLoading(false)
        }
      } catch (error) {
        setMessage('Something bug')
        setLoading(false)
      }
    } else {
      setLoading(true)
      try {
        const res = await axios.post(
          `${import.meta.env.VITE_BASE_URL}/UserService/resetpassword/${username}/${token}`,
          {
            newPassword: formData.newPassword
          }
        )
        if (res.status === 200) {
          setMessage('Change password success! Redirect Homepage after 2s!')
          setTimeout(() => {
            window.open('https://localhost:3000')
          }, 2000)
          setLoading(false)
        }
      } catch (error) {
        setMessage('Something Bug?')
        setLoading(false)
      }
    }
  }

  return (
    <div className='container'>
      <div className='w-[550px] h-[400px] mt-40 mx-auto bg-zinc-800 pt-8 rounded-[30px]'>
        <h1 className='text-3xl text-white text-center mb-10 mt-[45px] '>
          {currentPath === TYPEFORM.SENDMAIL ? 'Send Email To Reset Password' : 'Reset Your Password'}
        </h1>
        <form onSubmit={handleSubmit(typeForm)}>
          <div className='flex justify-center items-center'>
            <input
              type={currentPath === TYPEFORM.SENDMAIL ? 'email' : 'password'}
              name={currentPath === TYPEFORM.SENDMAIL ? 'email' : 'newPassword'}
              placeholder={
                currentPath === TYPEFORM.SENDMAIL ? 'Enter your email to reset password...' : 'Enter new password....'
              }
              className=' pl-3 h-9 w-72'
              onChange={handleChange}
              required
            />
          </div>
          <button
            className='w-72 bg-blue-800 text-white flex items-center justify-center gap-3 mx-auto mt-9 '
          // onClick={() => handleSubmit(typeForm)}
          >
            {currentPath === TYPEFORM.SENDMAIL ? 'Send Mail' : 'Reset Your Password'}
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
        </form>

        {message && <ErrorMessage errorMessage={message} />}
      </div>
    </div>
  )
}
