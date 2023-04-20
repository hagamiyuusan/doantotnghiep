import useClickOutSide from '../../helps/clickOutSide'
import styles from './LoginModal.module.css'
import './style.css'
import { useRef, useState } from 'react'
import { ErrorMessage } from '../ErrorMessage'
import axios from 'axios'
import jwt_decode from "jwt-decode";
// create modal login

interface IProps {
  showModalLogin: boolean
  setShowModalLogin: React.Dispatch<React.SetStateAction<boolean>>
}
interface IFormData {
  username: string
  email: string
  password: string
  confirm_password: string
}
const initFormData = {
  username: '',
  email: '',
  password: '',
  confirm_password: ''
}
enum TypeSubmit {
  LOGIN = 'LOGIN',
  REGISTER = 'REGISTER'
}
export default function LoginModal({ showModalLogin, setShowModalLogin }: IProps) {
  const loginModalRef = useRef(null)
  const [formData, setFormData] = useState<IFormData>(initFormData)
  const [errorMessage, setErrorMessage] = useState('')
  useClickOutSide(loginModalRef, () => {
    setShowModalLogin(false)
  })
  const handleChangeInput = (e: React.ChangeEvent<HTMLInputElement>) => {
    setErrorMessage('')
    setFormData((prev) => {
      return {
        ...prev,
        [e.target.name]: e.target.value
      }
    })
  }
  const handleSubmitForm = (typeSubmit: string) => async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    if (typeSubmit === TypeSubmit.REGISTER) {
      if (formData.password !== formData.confirm_password) {
        setErrorMessage('Enter again!')
        return
      }
      // handle Register
      const res = await axios.post('https://localhost:7749/api/UserService/register', {
        userName: formData.username,
        password: formData.password,
        email: formData.email
      })
      console.log(res)
    } else {
      // handle Login
      console.log('asdasdsad');
      const res = await axios.post('https://localhost:7749/api/UserService/authenticate', {
        UserName: formData.username,
        password: formData.password,
      });
      if (res.data) {
        console.log(jwt_decode(res.data?.token))
      }

    }
  }
  return (
    <>
      {showModalLogin && (
        <div className={styles.login_modal}>
          <div className='main' ref={loginModalRef}>
            <input type='checkbox' id='chk' aria-hidden='true' />

            <div className='signup'>
              <form className='signup_form' onSubmit={handleSubmitForm(TypeSubmit.REGISTER)}>
                <label htmlFor='chk' aria-hidden='true'>
                  Sign up
                </label>
                <input
                  type='text'
                  name='username'
                  placeholder='User name...'
                  required
                  value={formData.username}
                  onChange={handleChangeInput}
                  className='pl-3'
                />
                <input
                  type='email'
                  name='email'
                  placeholder='Email...'
                  required
                  value={formData.email}
                  onChange={handleChangeInput}
                  className='pl-3'
                />
                <input
                  type='password'
                  name='password'
                  placeholder='Password...'
                  required
                  value={formData.password}
                  onChange={handleChangeInput}
                  className='pl-3'
                />
                <input
                  type='password'
                  name='confirm_password'
                  placeholder='Confirm Password...'
                  required
                  value={formData.confirm_password}
                  onChange={handleChangeInput}
                  className='pl-3'
                />
                <ErrorMessage errorMessage={errorMessage} />
                <div className='flex justify-center items-center'>
                  <button type='submit' className='text-white border border-cyan-300'>
                    Sign up
                  </button>
                </div>
              </form>
            </div>

            <div className='login'>
              <form onSubmit={handleSubmitForm(TypeSubmit.LOGIN)}>
                <label htmlFor='chk' aria-hidden='true'>
                  Login
                </label>
                <input
                  type='text'
                  name='username'
                  placeholder='User Name...'
                  className=' mx-auto pl-2'
                  value={formData.username}
                  onChange={handleChangeInput} />
                <input
                  type='password'
                  name='password'
                  placeholder='Password'
                  className=' mx-auto pl-2 mt-3'
                  value={formData.password}
                  onChange={handleChangeInput} />
                <div className="min-h[1.25rem]"></div>
                <div className='flex justify-center items-center pt-6'>
                  <button type='submit' className='text-black border  border-black'>Login</button>
                </div>
              </form>
            </div>
          </div>
        </div>
      )}
    </>
  )
}
