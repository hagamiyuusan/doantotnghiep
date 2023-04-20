import useClickOutSide from '../../helps/clickOutSide'
import styles from './LoginModal.module.css'
import './style.css'
import { useRef, useState } from 'react'
import { ErrorMessage } from '../ErrorMessage'
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
  const handleSubmitForm = (typeSubmit: string) => (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    if (typeSubmit === TypeSubmit.REGISTER) {
      if (formData.password !== formData.confirm_password) {
        setErrorMessage('Enter again!')
        return
      }
      // handle Register
    }
    else {
      // handle Login
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
                />
                <ErrorMessage errorMessage={errorMessage} />
                <button type='submit'>Sign up</button>
              </form>
            </div>

            <div className='login'>
              <form onSubmit={handleSubmitForm(TypeSubmit.LOGIN)}>
                <label htmlFor='chk' aria-hidden='true'>
                  Login
                </label>
                <input type='email' name='email' placeholder='Email' />
                <input type='password' name='pswd' placeholder='Password' />
                <button type='submit'>Login</button>
              </form>
            </div>
          </div>
        </div>
      )}
    </>
  )
}
