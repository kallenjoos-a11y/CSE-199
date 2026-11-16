import './EventHeader.css'

function EventHeader({ children }){
return(
  <header className="site-header">
      <div className="header-inner">
        <div className="brand">
          <span className="brand-mark" aria-hidden="true">
            b<span>•</span>
            </span>BeThere<span className="brand-period">.</span>
        </div>
        {children}
      </div>
    </header>
  )
}

export default EventHeader