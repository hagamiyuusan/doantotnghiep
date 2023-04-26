import { useRef, useState } from 'react'
import useClickOutSide from '../../helps/clickOutSide'
import styles from './Header.module.css'
interface IProps {
  showModalLogin: boolean
  setShowModalLogin: React.Dispatch<React.SetStateAction<boolean>>
}
const Header = ({ setShowModalLogin }: IProps) => {
  const [openDropdownMenu, setOpenDropdownMenu] = useState<boolean>(false)
  const handleClick = () => {
    setOpenDropdownMenu((prev) => !prev)
  }
  const DropdownMenuRef = useRef(null)
  useClickOutSide(DropdownMenuRef, () => {
    setOpenDropdownMenu(false)
  })

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
        <div className=' flex gap-6'>
          <a
            href='#!'
            className={`${styles.action_btn} border text-yellow-500 border-yellow-300 hover:border-yellow-300  hover:text-zinc-50 `}
            onClick={() => setShowModalLogin(true)}
          >
            Try It Now!
          </a>
          <a
            href='#!'
            className={`${styles.action_btn} border border-white  text-white hover:text`}
            onClick={() => setShowModalLogin(true)}
          >
            Login
          </a>
        </div>

        <div className={styles.toggle_btn} onClick={handleClick} role='presentation'>
          <i className='fa-solid fa-bars text-stone-950	'></i>
        </div>
      </div>
      {/* Dropdown Menu */}
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
          <button className={styles.action_btn} onClick={() => setShowModalLogin(true)}>
            Login
          </button>
        </li>
      </ul>
    </header>
  )
}
export default Header
