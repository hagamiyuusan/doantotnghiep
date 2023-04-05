// interface IProps {
//   title: string
// }
// import styles from './Header.module.css'
import { useState } from 'react'
import styles from './Header.module.css'
const Header = () => {
  const [openDropdownMenu, setOpenDropdpwnMenu] = useState<boolean>(false)
  console.log(styles)
  const handleClick = () => {
    setOpenDropdpwnMenu(prev => !prev)
  }
  return (
    <header className={`${styles.header} container`}>
      <div className={styles.navbar}>
        <div className={styles.logo}>
          <a href="!#">DoAnTotNghiep</a>
        </div>
        <ul className={styles.nav_links}>
          <li><a href="!#">Home</a></li>
          <li><a href="!#">About</a></li>
          <li><a href="!#">Services</a></li>
          <li><a href="!#">Contact</a></li>
        </ul>
        <a href='!#' className={styles.action_btn}>
          Login
        </a>
        <div className={styles.toggle_btn} onClick={handleClick} role='presentation'>
          <i className='fa-solid fa-bars'></i>
        </div>
      </div>
      <ul className={`${styles.dropdown_menu} ${openDropdownMenu ? styles.open : ''}`}>
        <li><a href="!#">Home</a></li>
        <li><a href="!#">About</a></li>
        <li><a href="!#">Services</a></li>
        <li><a href="!#">Contact</a></li>
        <li><a href='!#' className={styles.action_btn}>Login</a></li>
      </ul>
    </header>
  )
}
export default Header
