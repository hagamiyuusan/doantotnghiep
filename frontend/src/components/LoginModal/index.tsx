import useClickOutSide from '~/helps/clickOutSide'
import styles from './LoginModal.module.css'
import './style.css'
import { useRef } from 'react'

interface IProps {
  showModalLogin: boolean
  setShowModalLogin: React.Dispatch<React.SetStateAction<boolean>>
}
export default function LoginModal({ showModalLogin, setShowModalLogin }: IProps) {
  const loginModalRef = useRef(null)
  useClickOutSide(loginModalRef, () => {
    setShowModalLogin(false)
  })
  return (
    <>
      {showModalLogin && (
        <div className={styles.login_modal}>
          <div className='main' ref={loginModalRef}>
            <input type='checkbox' id='chk' aria-hidden='true' />

            <div className='signup'>
              <form>
                <label htmlFor='chk' aria-hidden='true'>
                  Sign up
                </label>
                <input type='text' name='txt' placeholder='User name' />
                <input type='email' name='email' placeholder='Email' />
                <input type='password' name='pswd' placeholder='Password' />
                <button>Sign up</button>
              </form>
            </div>

            <div className='login'>
              <form>
                <label htmlFor='chk' aria-hidden='true'>
                  Login
                </label>
                <input type='email' name='email' placeholder='Email' />
                <input type='password' name='pswd' placeholder='Password' />
                <button>Login</button>
              </form>
            </div>
          </div>
        </div>
      )}
    </>
  )
}
