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
          <a href='!#'>LoGO</a>
        </div>
        <ul className={styles.nav_links}>
          <li>
            <a href='!#'>Home</a>
          </li>
          <li>
            <a href='!#'>About</a>
          </li>
          <li>
            <a href='!#'>Services</a>
          </li>
          <li>
            <a href='!#'>Contact</a>
          </li>
        </ul>
        {!user.isAuthenticated ? (
          <div className=' flex gap-6'>
            <a
              href='#!'
              className={`${styles.action_btn} border text-yellow-500 border-yellow-300 hover:border-yellow-300  hover:text-zinc-50 `}
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
                className={`${styles.action_btn} border text-yellow-500 border-yellow-300 hover:border-yellow-300  hover:text-zinc-50 `}
                onClick={scrollToOCR}
              >
                Try It Now!
              </button>
              <a
                href='#!'
                // className={`${styles.action_btn} border border-white  text-white hover:text`}
                className='ml-2'
                onClick={() => setOpenDropdownUser(true)}
              >
                Hi! {user.profile?.userName}
              </a>
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
          <a href='!#'>Home</a>
        </li>
        <li>
          <a href='!#'>About</a>
        </li>
        <li>
          <a href='!#'>Services</a>
        </li>
        <li>
          <a href='!#'>Contact</a>
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
          <li>
            <Link to='/manageruser'>User Manager</Link>
          </li>
          <li>
            <Link to='/managerproduct'>Manager User</Link>
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
                  user.reset(), setOpenDropdownUser(false), console.log(openDropdownMenu)

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
            {!user.isAuthenticated ? (
              <button className={styles.action_btn} onClick={() => setShowModalLogin(true)}>
                Login
              </button>
            ) : (
              <button className={styles.action_btn} onClick={() => { user.reset(), setOpenDropdownUser(false) }}>
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
