import useClickOutSide from '../../helps/clickOutSide'
import styles from './LoginModal.module.css'
import './style.css'
import { useContext, useRef, useState } from 'react'
import { ErrorMessage } from '../ErrorMessage'
import axios from 'axios'
import jwt_decode from 'jwt-decode'
import { AppContext } from '../../Context/context'
import { Link } from 'react-router-dom'
import { toast } from 'react-toastify'
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
  const appContext = useContext(AppContext)

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
    const regex = /^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{8,}$/

    if (typeSubmit === TypeSubmit.REGISTER) {
      if (formData.username.length <= 8) {
        setErrorMessage('Uername must be more than 8 characters')
        return
      }
      if (!regex.test(formData.password)) {
        setErrorMessage('Password must contain at least 1 uppercase letter, 1 number and 1 special character ')
        return
      }
      if (formData.password !== formData.confirm_password) {
        setErrorMessage('Confirm password not match!')
        return
      }
      // handle Register
      try {
        const res = await axios.post('https://localhost:7749/api/UserService/register', {
          userName: formData.username,
          password: formData.password,
          email: formData.email
        })
        if (res.status === 200) {
          toast.success('Register is success !', {
            position: toast.POSITION.TOP_RIGHT
          });
          appContext.setProfile(res.data)
          setErrorMessage(`Check your email to verify account`)
          // setShowModalLogin(false)
        }
      } catch (error: any) {
        toast.error('Register is failed! Try again', {
          position: toast.POSITION.TOP_RIGHT
        });
        setErrorMessage('Something Bug?')
        // setErrorMessage(error.response.data)
      }
    } else {
      // handle Login
      try {
        const res = await axios.post('https://localhost:7749/api/UserService/authenticate', {
          UserName: formData.username,
          password: formData.password
        })
        if (res.data) {
          toast.success('Login Success !', {
            position: toast.POSITION.TOP_RIGHT
          });
          appContext.setProfile(jwt_decode(res.data?.token))
          appContext.setIsAuthenticated(true)
          console.log(jwt_decode(res.data?.token))
          localStorage.setItem('access_token', res.data?.token)
          setShowModalLogin(false)

        }
      } catch (error: any) {
        toast.error('Login failed!', {
          position: toast.POSITION.TOP_RIGHT
        });

        setErrorMessage(error?.response?.data)
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
                  <button type='submit' className='text-white border border-cyan-300 w-7/12'>
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
                  onChange={handleChangeInput}
                  required
                />
                <input
                  type='password'
                  name='password'
                  placeholder='Password'
                  className=' mx-auto pl-2 mt-3'
                  value={formData.password}
                  onChange={handleChangeInput}
                  required
                />
                <p className='text-center mt-4'>
                  Forget password?
                  <Link target='_blank' to='/sendMailChangePassword' className='text-blue-700'>
                    Click Here!!
                  </Link>
                </p>
                <ErrorMessage errorMessage={errorMessage} />
                <div className='flex justify-center items-center '>
                  <button type='submit' className='text-black border  border-black w-7/12'>
                    Login
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      )}
    </>
  )
}
