interface IProps {
  name: string
  position: string
  img: string
  review: string
}

export default function ReviewView(props: IProps) {
  return (
    <div className=' bg-zinc-900 flex items-center justify-center mt-6'>
      <div className=' w-7/12 animate-loading'>
        <img src={props.img} alt='' className='w-full object-cover object-center animate-loading' />
      </div>
      <div className='w-full p-1 animate-loading'>
        <p className=' text-sm text-white mb-3'>{props.name}</p>
        <p className='text-white mb-3 text-base'>{props.position}</p>
        <p className='text-white font-serif leading-6'>{props.review}</p>
      </div>


    </div>
  )
}
