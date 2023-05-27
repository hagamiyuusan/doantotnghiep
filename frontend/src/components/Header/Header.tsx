/* eslint-disable jsx-a11y/anchor-is-valid */
import { useContext, useRef, useState } from 'react'
import useClickOutSide from '../../helps/clickOutSide'
import styles from './Header.module.css'
import { AppContext, AppContextInterface } from 'src/Context/context'
import { Link } from 'react-router-dom'
interface IProps {
  user: AppContextInterface
  showModalLogin: boolean
  setShowModalLogin: React.Dispatch<React.SetStateAction<boolean>>
}
const Header = ({ setShowModalLogin, user }: IProps) => {
  const [openDropdownMenu, setOpenDropdownMenu] = useState<boolean>(false)
  const [openDropdownUser, setOpenDropdownUser] = useState<boolean>(false)
  const { ocrRef, profile } = useContext(AppContext)
  const handleClick = () => {
    setOpenDropdownMenu((prev) => !prev)
  }
  const DropdownMenuRef = useRef(null)
  useClickOutSide(DropdownMenuRef, () => {
    setOpenDropdownMenu(false)
    setOpenDropdownUser(false)
  })
  const scrollToOCR = () => {
    if (ocrRef.current) {
      // Perform the desired action using ocrRef.current
      ocrRef.current.scrollIntoView({ behavior: 'smooth' })
    }
  }
  return (
    <header className={`${styles.header} mb-16`}>
      <div className={styles.navbar}>
        <div className={styles.logo}>
          <a href='https://localhost:3000'>LoGO</a>
        </div>
        <ul className={styles.nav_links}>
          <li>
            <a href='https://localhost:3000'>Home</a>
          </li>
          <li>
            <a href='#'>About</a>
          </li>
          <li>
            <a href='#'>Services</a>
          </li>
          <li>
            <a href='https://www.facebook.com/Hau.VT07' target='_blank'>
              Contact
            </a>
          </li>
        </ul>
        {!user.isAuthenticated ? (
          <div className=' flex gap-6'>
            <a
              href='#!'
              className={`${styles.action_btn} ${styles.blink} border text-yellow-500 border-yellow-300 hover:border-yellow-300  hover:text-zinc-50 `}
              onClick={scrollToOCR}
            >
              Try It Now!
            </a>
            <button
              className={`${styles.action_btn} border border-white  text-white hover:text`}
              onClick={() => setShowModalLogin(true)}
            >
              Login
            </button>
          </div>
        ) : (
          <>
            <div className=''>
              <button
                className={`${styles.action_btn} ${styles.blink} bg-yellow-800 border text-yellow-500 border-yellow-300 hover:border-yellow-300  hover:text-zinc-50 `}
                onClick={scrollToOCR}
              >
                Try It Now!
              </button>
              <span
                role='button'
                // className={`${styles.action_btn} border border-white  text-white hover:text`}
                className='ml-2 text-white'
                onClick={() => setOpenDropdownUser(true)}
                onKeyDown={() => {
                  console.log()
                }}
                tabIndex={0}
              >
                Hi! {user.profile?.userName}
              </span>
            </div>
            <div className={styles.toggle_btn} onClick={handleClick} role='presentation'>
              <i className='fa-solid fa-bars text-stone-950	'></i>
            </div>
          </>
        )}

        {/* User Login */}
      </div>
      {/* Dropdown Menu when reponsive*/}
      <ul className={`${styles.dropdown_menu} ${openDropdownMenu ? styles.open : ''} `} ref={DropdownMenuRef}>
        <li>
          <a href='http://127.0.0.1:3000/'>Home</a>
        </li>
        <li>
          <a href='#'>About</a>
        </li>
        <li>
          <a href='#'>Services</a>
        </li>
        <li>
          <a href='#'>Contact</a>
        </li>
        <li>
          {!user.isAuthenticated ? (
            <button className={styles.action_btn} onClick={() => setShowModalLogin(true)}>
              Login
            </button>
          ) : (
            <button className={styles.action_btn} onClick={() => setShowModalLogin(true)}>
              Logout
            </button>
          )}
        </li>
      </ul>
      {/* Dropdown User When Login */}

      {/* Dropdown Admin */}
      {profile?.role === 'admin' ? (
        <ul className={`${styles.dropdown_menu} ${openDropdownUser ? styles.open : ''} `} ref={DropdownMenuRef}>
          <li className='hover:bg-slate-600'>
            <Link to='/admin/usermanager' className='hover:bg-slate-600 hover:text-white'>
              User Manager
            </Link>
          </li>
          <li>
            <Link to='/admin/durationmanager' className='hover:bg-slate-600 hover:text-white'>
              Duration Manager
            </Link>
          </li>
          <li>
            <Link to='/admin/productmanager' className='hover:bg-slate-600 hover:text-white'>
              Product Manager
            </Link>
          </li>
          <li>
            <Link to='/historypurchase' className='hover:bg-slate-600 hover:text-white'>
              History Purchase
            </Link>
          </li>
          <li>
            {!user.isAuthenticated ? (
              <button className={styles.action_btn} onClick={() => setShowModalLogin(true)}>
                Login
              </button>
            ) : (
              <button
                className={styles.action_btn}
                onClick={() => {
                  user.reset(), setOpenDropdownUser(false)
                }}
              >
                Logout
              </button>
            )}
          </li>
        </ul>
      ) : (
        <ul className={`${styles.dropdown_menu} ${openDropdownUser ? styles.open : ''} `} ref={DropdownMenuRef}>
          <li>
            <Link to='/profile'>Go To Profile</Link>
          </li>
          <li>
            <Link to='/historypurchase'>History Purchase</Link>
          </li>
          <li>
            {!user.isAuthenticated ? (
              <button className={styles.action_btn} onClick={() => setShowModalLogin(true)}>
                Login
              </button>
            ) : (
              <button
                className={styles.action_btn}
                onClick={() => {
                  user.reset(), setOpenDropdownUser(false)
                }}
              >
                Logout
              </button>
            )}
          </li>
        </ul>
      )}
    </header>
  )
}
export default Header
